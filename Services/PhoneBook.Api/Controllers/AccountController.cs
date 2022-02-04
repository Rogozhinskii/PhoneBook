using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger,UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        /// <summary>
        /// Выполняет регистрацию пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("registration")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            _logger.LogInformation($">>> Рестрация пользователя. Пользователь {model.LoginProp}");
            var user = new User { UserName = model.LoginProp };
            var createResult=await _userManager.CreateAsync(user,model.Password);
            if (createResult.Succeeded)
            {
                _logger.LogInformation($">>> Пользователь {model.LoginProp} зарегистрирован");
                await _signInManager.SignInAsync(user,false);
                return Ok(createResult);
            }
            else
            {
                return BadRequest(createResult);
            }
        }


        /// <summary>
        /// Выполняет вход пользователя в систему
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost("login")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            _logger.LogInformation($">>> Попытка входа в систему. Пользователь {userLogin.UserName}");
            var loginResult=await _signInManager.PasswordSignInAsync(userLogin.UserName,userLogin.Password,false,false);            
            if (loginResult.Succeeded)
            {
                _logger.LogInformation($">>> Вход выполнен. Пользователь {userLogin.UserName}");
                return Ok(loginResult);
            }
            else
            {
                _logger.LogInformation($">>> Ошибка входа в систему. Пользователь {userLogin.UserName}");
                return BadRequest(loginResult);
            }   
        }

        /// <summary>
        /// Возвращает роль пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("getRole/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserRoleAsync(string userName)
        {
            _logger.LogInformation($">>> Попытка получение роли пользователя {userName}.");
            var user=await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogInformation($">>> Пользователь: {userName} не найден.");
                return BadRequest("user not found");
            }
            var userRole=(await _userManager.GetRolesAsync(user).ConfigureAwait(false)).FirstOrDefault();
            if (userRole is null)
            {
                _logger.LogInformation($">>> Пользователь: {userName}. Роли не найдены.");
                return BadRequest("User role not found");
            }
            _logger.LogInformation($">>> Пользователь: {userName}. Роли найдена {userRole}.");
            return Ok(userRole);
        }


        /// <summary>
        /// Выход из учетной записи
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($">>> Выход  пользователя из приложения");
            await _signInManager.SignOutAsync();
            return Ok();
        }


    }
}
