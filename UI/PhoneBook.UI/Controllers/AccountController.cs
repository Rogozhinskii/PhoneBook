using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    /// <summary>
    /// Контроллер аутентификации пользователя
    /// </summary>
    public class AccountController : Controller
    { 

        private readonly IAuthentificationService _authentificationService;

        public AccountController(SignInManager<User> signInManager, IAuthentificationService authentificationService)=>
            _authentificationService = authentificationService;            
        

        [HttpGet]
        public IActionResult Login()=>
            View();
        
        
        [HttpPost, ValidateAntiForgeryToken]        
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _authentificationService.Login(login);
                if (loginResult.Success)
                {
                    HttpContext.Session.SetString("Token", loginResult.Token);
                    HttpContext.Session.SetString("Role", loginResult.Role);
                    HttpContext.Session.SetString("UserName", loginResult.UserName);
                    return RedirectToAction("Index", "PhoneRecords");
                }

            }
            ModelState.AddModelError("", "Пользователь не найден");
            return View(login);           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "PhoneRecords");
        }

        public IActionResult AccessDenied()=>
            View();
        
    }
}
