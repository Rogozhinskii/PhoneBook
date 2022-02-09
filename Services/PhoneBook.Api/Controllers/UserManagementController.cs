using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Models;
using PhoneBook.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserManagementController(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("getUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList= await _userManager.Users.Select(u => new UserInfo
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToArrayAsync();
            if(userList.Any())
                return Ok(userList);
            return NotFound();
        }


        [HttpPost("addNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(UserInfo model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult creationResult = await _userManager.CreateAsync(user, model.Password);
                if (creationResult.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByNameAsync(UserRoles.RegularUser).ConfigureAwait(false);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name).ConfigureAwait(false);
                        if (roleResult.Succeeded)
                        {
                            return Ok(creationResult.Succeeded);
                        }
                    }
                }
            }                 
            return BadRequest(ModelState);
        }

        [HttpGet("getUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetUserById(string id) =>
            await _userManager.FindByIdAsync(id) is { } item
            ? Ok(item)
            : NotFound();

        [HttpGet("getUserRoles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            return Ok(roles);
        }

        [HttpPost("updateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> UpdateUser(User model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            user.UserName = model.UserName;
            user.Email = model.Email;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Succeeded);
        }

        [HttpPost("removeFromRole/{existingRole}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> RemoveFromUser(User model,string existingRole)
        {
            //User user = await _userManager.FindByIdAsync(model.Id);
            var result = await _userManager.RemoveFromRoleAsync(model, existingRole);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Succeeded);
        }

        [HttpPost("addToRole/{newRole}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> AddToRole(User model, string newRole)
        {
            //User user = await _userManager.FindByIdAsync(model.Id);
            var result = await _userManager.AddToRoleAsync(model, newRole);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Succeeded);
        }



        [HttpGet("getRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetRoles()
        {
            var rolesList =await _roleManager.Roles.ToArrayAsync();
            if(rolesList.Any())
                return Ok(rolesList);
            return NotFound(Enumerable.Empty<ApplicationRole>());
        }
       
        
        [HttpGet("getRole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetRoleById(string id) =>
            await _roleManager.FindByIdAsync(id) is { } item
            ? Ok(item)
            : NotFound();


        [HttpPost("updateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> UpdateRole(ApplicationRole item)
        {
            var result = await _roleManager.UpdateAsync(item).ConfigureAwait(false);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result);
        }


        [HttpPost("createRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> CreateRole(ApplicationRole item)
        {
            var result = await _roleManager.CreateAsync(item).ConfigureAwait(false);
            if(result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result);
        }

        [HttpDelete("deleteRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> DeleteRole(ApplicationRole item)
        {           
            var result = await _roleManager.DeleteAsync(item).ConfigureAwait(false);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Errors);
        }

        [HttpGet("getRoleId/{roleName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetRoleIdByName(string roleName)
        {
            var result = (await _roleManager.Roles.SingleAsync(r => r.Name == roleName).ConfigureAwait(false)).Id;
            if (result is not null)
                return Ok(result);
            return BadRequest();
        }


        [HttpPost("deleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Errors);
        }

    }
}
