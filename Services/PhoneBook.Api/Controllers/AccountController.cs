using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
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
            var loginResult=await _signInManager.PasswordSignInAsync(userLogin.LoginProp,userLogin.Password,false,false);
            if (loginResult.Succeeded)
                return Ok(loginResult);
            else
                return BadRequest(loginResult);
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
