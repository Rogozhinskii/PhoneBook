using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Automapper;
using PhoneBook.Entities;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    
    public class PhoneRecordsController : Controller
    {
        
        private readonly IMappedRepository<PhoneRecordViewModel, PhoneRecord> _repository;

        public PhoneRecordsController(IMappedRepository<PhoneRecordViewModel,PhoneRecord> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageIndex,int? pageSize)
        {            
            if (searchString != null){
                pageIndex = 0;
                ViewData["CurrentFilter"] = searchString;
                return View(await _repository.GetPage(x => x.FirstName.Contains(searchString,StringComparison.InvariantCultureIgnoreCase)
                                                        || x.LastName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)));
            }
            else{
                searchString = currentFilter;
            }
            ViewData["pageSize"] = pageSize;
            ViewData["CurrentFilter"] = searchString;
            return View(await _repository.GetPage(pageIndex ?? 0, pageSize??5));            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {           
            if (id is null) return NotFound();
            return View(await _repository.GetById(id.Value));
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {                    
            if(await _repository.DeleteById(id) is null) return NotFound();
            TempData["SuccessMessage"] = $"Record deleted";
            return Redirect("~/");
        }
                

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();                          
            if (await _repository.GetById(id.Value) is { } record){
                return View(record);
            }
            return NotFound();
        }
    }
}
