using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands;
using PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands.UsersCommands;
using PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.RolesQueries;
using PhoneBook.CommandsAndQueries.Queries.UsersAndRolesQueries.UsersQueries;
using PhoneBook.Common.Models;
using PhoneBook.Domain;
using PhoneBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IMediator _mediator;        
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;            
            _mapper = mapper;
        }

        private UserInfo GetItem(User item) => _mapper.Map<UserInfo>(item);
        private User GetBase(UserInfo item) => _mapper.Map<User>(item);
        private IEnumerable<UserInfo> GetItem(IEnumerable<User> items) => _mapper.Map<IEnumerable<UserInfo>>(items);
        



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = (IEnumerable<User>)await _mediator.Send(new GetAllUsersQuery { Token = GetToken() });
            return View(GetItem(users));
        }

        private string GetToken() =>
           HttpContext.Session.GetString("Token");


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddUser() =>
            View(new UserInfo());


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(UserInfo model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password.Length < 6)
                    return ValidationProblem();
                var result = await _mediator.Send(new AddNewUserCommand { User=model, Token = GetToken() });
                if (result)
                {
                    return Redirect("~/");                    
                }
            }
            return BadRequest(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new();
            model.ApplicationRoles = ((IEnumerable<ApplicationRole>)await _mediator.Send(new GetRolesQuery { Token = GetToken() })).Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _mediator.Send(new GetUserByIdQuery { Id = id, Token = GetToken() });
                if (user is null) return NotFound($"User not found: id {id}");
                var userRole = (await _mediator.Send(new GetUserRoles { UserId = id, Token = GetToken() })).FirstOrDefault();
                if (userRole is null) return BadRequest("User has no role");
                model.UserName = user.UserName;
                model.Email = user.Email;
                model.ApplicationRoleId = model.ApplicationRoles.FirstOrDefault(x=>x.Text==userRole).Value;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _mediator.Send(new GetUserByIdQuery { Id = id, Token = GetToken() });
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    string existingRole = (await _mediator.Send(new GetUserRoles { UserId = id, Token = GetToken() })).FirstOrDefault();
                    string existingRoleId = await _mediator.Send(new GetRoleIdByName { RoleName = existingRole, Token = GetToken() });
                    var result = await _mediator.Send(new UpdateUserCommand { User = user, Token = GetToken() });
                    if (result)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            var roleResult = await _mediator.Send(new RemoveFromRoleCommand { User = user, ExistingRole = existingRole, Token = GetToken() });
                            if (roleResult)
                            {
                                var newRole = await _mediator.Send(new GetRoleByIdQuery { Id = model.ApplicationRoleId, Token = GetToken() });
                                if (newRole != null)
                                {
                                    var newRoleResult = await _mediator.Send(new AddToRoleCommand { NewRoleName = newRole.Name, Token = GetToken(), User = user });
                                    if (newRoleResult)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return BadRequest(model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var applicationUser = await _mediator.Send(new GetUserByIdQuery { Id = id, Token = GetToken() });// await _userManager.FindByIdAsync(id);
            var user = new User
            {
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                Id = applicationUser.Id
            };
            return View(user);
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("id is null");
            var result = await _mediator.Send(new DeleteUserByIdCommand { Id = id, Token = GetToken() });
            if (result)
                return RedirectToAction("Index");
            return BadRequest("Something wrong");
        }

    }
}
