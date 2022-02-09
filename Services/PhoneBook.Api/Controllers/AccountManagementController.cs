using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.Api.Helpers;
using PhoneBook.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly ILogger<AccountManagementController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtConfiguration _jwtConfig;

        public AccountManagementController(ILogger<AccountManagementController> logger,UserManager<User> userManager, SignInManager<User> signInManager, IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = optionsMonitor.CurrentValue;
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
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(userLogin.UserName);

                if (existingUser == null)
                {
                    return BadRequest(new AuthentificationResult()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userLogin.Password);
                var userRole = (await _userManager.GetRolesAsync(existingUser)).FirstOrDefault();

                if (!isCorrect)
                {
                    return BadRequest(new AuthentificationResult()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser, userRole);

                return Ok(new AuthentificationResult()
                {
                    UserName = existingUser.UserName,
                    Success = true,
                    Token = jwtToken,
                    Role=userRole
                });
            }

            return BadRequest(new AuthentificationResult()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        private string GenerateJwtToken(User user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email), //todo удалить
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
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
