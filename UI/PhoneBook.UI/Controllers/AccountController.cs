using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    /// <summary>
    /// Контроллер аутентификации пользователя
    /// </summary>
    public class AccountController : Controller
    {       
        
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)=>
            _signInManager = signInManager;
        


        [HttpGet]
        public IActionResult Login()=>
            View();
        
        
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(login.UserName,
                                                                           login.Password,
                                                                           false,
                                                                           lockoutOnFailure: false);
                if (loginResult.Succeeded)
                    return RedirectToAction("Index", "PhoneRecords");
            }
            ModelState.AddModelError("", "Пользователь не найден");
            return View(login);                     
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "PhoneRecords");
        }

        public IActionResult AccessDenied()=>
            View();
        
    }
}
