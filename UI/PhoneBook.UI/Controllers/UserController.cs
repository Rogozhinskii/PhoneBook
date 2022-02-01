using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBook.Common.Models;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
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
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = _userManager.Users.Select(u => new UserListViewModel
            {               
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email                
            }).ToList();
            return View(model);
        }

       

        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();           
            return View("AddUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {                
                User user = new User
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email                    
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    ApplicationRole applicationRole = await _roleManager.FindByNameAsync(UserRole.RegularUser.ToString()).ConfigureAwait(false);                    
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index", "PhoneRecords");
                        }
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                
                User user = await _userManager.FindByIdAsync(id);                
                if (user != null)
                {
                    model.Name = user.Name;
                    model.Email = user.Email;
                    model.ApplicationRoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id;
                }
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
                    user.Name = model.Name;
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
