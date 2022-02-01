using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Threading.Tasks;

namespace PhoneBook.Controllers
{
    public class AccountController : Controller
    {        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(login.LoginProp,
                    login.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }

                    return RedirectToAction("Index", "PhoneRecords");
                }

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
    }
}
