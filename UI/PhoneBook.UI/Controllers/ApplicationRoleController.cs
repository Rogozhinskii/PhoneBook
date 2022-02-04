using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using PhoneBook.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    /// <summary>
    /// Контроллер управления ролями пользователей
    /// </summary>
    [Authorize(Roles =UserRoles.Administrator)]
    public class ApplicationRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public ApplicationRoleController(RoleManager<ApplicationRole> roleManager, IMapper mapper) {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        
        private ApplicationRoleViewModel GetItem(ApplicationRole item)=>_mapper.Map<ApplicationRoleViewModel>(item);
       
        /// <summary>
        /// Возвразает представление наполненное зарегистрированными ролями 
        /// </summary>
        /// <returns></returns>
        public IActionResult Roles()=>
            View(_roleManager.Roles.Select(r => new ApplicationRoleViewModel
                                                    {
                                                        Name = r.Name,
                                                        Id = r.Id,
                                                        Description = r.Description,

                                                    }).ToList());
        
        
        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new();
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole is null) return BadRequest("Role not found.");
                return View(GetItem(applicationRole));

            }          
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !string.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(id) :
               new ApplicationRole
               {
                   CreatedDate = DateTime.UtcNow
               };
                applicationRole.Name = model.Name;
                applicationRole.Description = model.Description;                
                IdentityResult roleRuslt = isExist ? await _roleManager.UpdateAsync(applicationRole)
                                                    : await _roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
            }
            return BadRequest(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            if(id is null) return NotFound();
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole is null) return NotFound();            
            return View(GetItem(applicationRole));            
        }

        [HttpPost]
        [ActionName("DeleteApplicationRole")]
        public async Task<IActionResult> DeleteApplicationRoleConfirmed(string id)
        {
            if (id is null) return NotFound();
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole is null) return NotFound();
            IdentityResult roleRuslt = await _roleManager.DeleteAsync(applicationRole);
            if (roleRuslt.Succeeded)
                return Redirect("Roles");
            return BadRequest("Объект не удален");

        }
    }
}
