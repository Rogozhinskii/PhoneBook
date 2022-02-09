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
    /// <summary>
    /// Контроллер для работы с ролями и пользователями
    /// </summary>
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


        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Добавляет пользователя в систему (пришлось разделить с регистрацией)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
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

        /// <summary>
        /// Возвращает пользователя по его идентификационному номеру
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetUserById(string id) =>
            await _userManager.FindByIdAsync(id) is { } item
            ? Ok(item)
            : NotFound();

        /// <summary>
        /// Возвращает роль пользователя по id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Выполняет обновление пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Удаляет роль у пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="existingRole"></param>
        /// <returns></returns>
        [HttpPost("removeFromRole/{existingRole}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> RemoveFromUser(User model,string existingRole)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            var result = await _userManager.RemoveFromRoleAsync(user, existingRole);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Succeeded);
        }

        /// <summary>
        /// Добавляет роль пользователю
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        [HttpPost("addToRole/{newRole}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> AddToRole(User model, string newRole)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            var result = await _userManager.AddToRoleAsync(user, newRole);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result.Succeeded);
        }


        /// <summary>
        /// Возвращает перечисление ролей доступных в системе
        /// </summary>
        /// <returns></returns>
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
       
        /// <summary>
        /// Возвращает роль по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getRole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetRoleById(string id) =>
            await _roleManager.FindByIdAsync(id) is { } item
            ? Ok(item)
            : NotFound();

        /// <summary>
        /// Выполняет обновление параметров роли 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("updateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> UpdateRole(ApplicationRole item)
        {
            var role = await _roleManager.FindByIdAsync(item.Id);
            role.Name=item.Name;
            role.Description=item.Description;
            var result = await _roleManager.UpdateAsync(role).ConfigureAwait(false);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return BadRequest(result);
        }

        /// <summary>
        /// Создает новую роль
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Удаляет роль
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// возвращает идентификационный номер роли по ее названию
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Выполняет удаление пользователя из хранилища
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
