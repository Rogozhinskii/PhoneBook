using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    /// <summary>
    /// Контроллер для проверки прав у пользователя на выполнение того или иного действия 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        [HttpGet("canedit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =UserRoles.Administrator)]
        public async Task<IActionResult> CanEdit()
        {
            return await Task.Run(()=>Ok());
        }

        [HttpGet("candelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator)]
        public async Task<IActionResult> CanDelete()
        {
            return await Task.Run(() => Ok());
        }

        [HttpGet("cancreate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = UserRoles.Administrator + "," + UserRoles.RegularUser)]
        public async Task<IActionResult> CanCreate()
        {
            return await Task.Run(() => Ok());
        }

    }
}
