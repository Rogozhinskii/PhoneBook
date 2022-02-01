using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class ApplicationRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public ApplicationRoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager=roleManager;
        }


        public IActionResult Roles()
        {
            List<ApplicationRoleListViewModel> model = new List<ApplicationRoleListViewModel>();
            
            
            model = _roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
            }
            return View("AddEditApplicationRole", model);
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
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;                
                IdentityResult roleRuslt = isExist ? await _roleManager.UpdateAsync(applicationRole)
                                                    : await _roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            if(id is null) return NotFound();
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole is null) return NotFound();
            ApplicationRoleViewModel model = new()
            {
                Id = applicationRole.Id,
                RoleName = applicationRole.Name,
                Description = applicationRole.Description
            };
            return View(model);            
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
