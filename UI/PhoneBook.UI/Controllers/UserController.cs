using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBook.Common.Models;
using PhoneBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    [Authorize(Roles =UserRoles.Administrator)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        

        public UserController(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            
        }

        [HttpGet]
        public IActionResult Index()=>
            View(_userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToList());


       
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddUser()=>
            View(new UserViewModel());
        

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {     
                User user = new()
                {                    
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByNameAsync(UserRoles.RegularUser).ConfigureAwait(false);                    
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return BadRequest(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            if (!string.IsNullOrEmpty(id))
            {   
                User user = await _userManager.FindByIdAsync(id);
                if (user is null) return NotFound($"User not found: id {id}");
                var userRole=(await _userManager.GetRolesAsync(user).ConfigureAwait(false)).FirstOrDefault();
                if (userRole is null) return BadRequest("User has no role");
                model.UserName = user.UserName;
                model.Email = user.Email;
                model.ApplicationRoleId = _roleManager.Roles.Single(r => r.Name == userRole).Id;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(id); 
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    string existingRole = (await _userManager.GetRolesAsync(user)).Single();
                    string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return BadRequest(model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User applicationUser = await _userManager.FindByIdAsync(id);            
            return View(applicationUser);
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("id is null");
            User applicationUser = await _userManager.FindByIdAsync(id);
            if(applicationUser is null) return NotFound("User not found");
            IdentityResult result = await _userManager.DeleteAsync(applicationUser);
            if (result.Succeeded)
                return RedirectToAction("Index");
            return BadRequest("Something wrong");
        }

    }
}
