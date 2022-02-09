using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CommandsAndQueries.Commands.UsersAndRolesCommands;
using PhoneBook.Domain;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    /// <summary>
    /// Контроллер управления ролями пользователей
    /// </summary>    
    public class ApplicationRoleController : Controller
    {        
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ApplicationRoleController(IMediator mediator, IMapper mapper) {
            
            _mediator = mediator;
            _mapper = mapper;
        }
        
        private ApplicationRoleViewModel GetItem(ApplicationRole item)=>_mapper.Map<ApplicationRoleViewModel>(item);
        private IEnumerable<ApplicationRoleViewModel> GetItem(IEnumerable<ApplicationRole> items) => _mapper.Map<IEnumerable<ApplicationRoleViewModel>>(items);

        /// <summary>
        /// Возвразает представление наполненное зарегистрированными ролями 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Roles(){
            var roles= (IEnumerable<ApplicationRole>)await _mediator.Send(new GetRolesQuery { Token = GetToken() });
            return View(GetItem(roles));
        }
      
        private string GetToken() =>
            HttpContext.Session.GetString("Token");


        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new();
            if (!string.IsNullOrEmpty(id))
            {
                var applicationRole =(ApplicationRole)await _mediator.Send(new GetRoleByIdQuery { Id = id, Token = GetToken() }); //await _roleManager.FindByIdAsync(id);
                if (applicationRole is null) return BadRequest("Role not found.");
                return View(GetItem(applicationRole));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid){
                bool isExist = !string.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? (ApplicationRole) await _mediator.Send(new GetRoleByIdQuery { Id = id, Token = GetToken() }) 
                                                         : new ApplicationRole
                                                           {
                                                               CreatedDate = DateTime.UtcNow
                                                           };                                
                 applicationRole.Name = model.Name;
                 applicationRole.Description = model.Description;
                var roleRuslt = isExist ? await _mediator.Send(new UpdateRoleCommand { Role = applicationRole, Token = GetToken() })
                                                   : await _mediator.Send(new CreateRoleCommand { Role = applicationRole, Token = GetToken() });
                if (roleRuslt){
                    return RedirectToAction("Roles");
                }
            }
            return BadRequest(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            if (id is null) return NotFound();
            var applicationRole = (ApplicationRole)await _mediator.Send(new GetRoleByIdQuery { Id = id, Token = GetToken() }); 
            if (applicationRole is null) return NotFound();
            return View(GetItem(applicationRole));
        }

        [HttpPost]
        [ActionName("DeleteApplicationRole")]
        public async Task<IActionResult> DeleteApplicationRoleConfirmed(string id)
        {
            if (id is null) return NotFound();
            var applicationRole = (ApplicationRole)await _mediator.Send(new GetRoleByIdQuery { Id = id, Token = GetToken() });
            if (applicationRole is null) return NotFound();
            var deleteResult = await _mediator.Send(new DeleteRoleCommand { Role = applicationRole, Token = GetToken() });//await _roleManager.DeleteAsync(applicationRole);
            if (deleteResult)
                return Redirect("Roles");
            return BadRequest("Объект не удален");
        }
    }
}
