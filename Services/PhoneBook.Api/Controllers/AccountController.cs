using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("registration")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            var user = new User { UserName = model.LoginProp };
            var createResult=await _userManager.CreateAsync(user,model.Password);
            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(user,false);
                return Ok(createResult);
            }
            else
            {
                return BadRequest(createResult);
            }
        }

        [HttpPost("login")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var loginResult=await _signInManager.PasswordSignInAsync(userLogin.UserName,userLogin.Password,false,false);            
            if (loginResult.Succeeded)
                return Ok(loginResult);
            else
                return BadRequest(loginResult);
        }

        [HttpGet("getRole/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserRoleAsync(string userName)
        {
            var user=await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null) return BadRequest("user not found");
            var userRole=(await _userManager.GetRolesAsync(user).ConfigureAwait(false)).FirstOrDefault();
            if (userRole is null) return BadRequest("User role not found");
            return Ok(userRole);
        }



        [HttpGet("logout")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


    }
}
