using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;
using System.Diagnostics;

namespace PhoneBook.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            var exeprtionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();            
            var errorViewModel = new ErrorViewModel
            {
                RequestId=Activity.Current?.Id,
                Path = exeprtionHandler.Path,
                Message = exeprtionHandler.Error.Message,
                StackTrace = exeprtionHandler.Error.StackTrace,
                StatusCode=HttpContext.Response.StatusCode
            };            
            return View(errorViewModel);
        }
    }
}
