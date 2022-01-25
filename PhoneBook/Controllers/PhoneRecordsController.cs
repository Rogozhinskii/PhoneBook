using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common;
using PhoneBook.Entities;
using PhoneBook.Interfaces;


namespace PhoneBook.Controllers
{
    public class PhoneRecordsController : Controller
    {
        private readonly IRepository<PhoneRecord> _repository;

        public PhoneRecordsController(IRepository<PhoneRecord> repository)
        {
            _repository = repository;
        }

        // GET: PhoneRecords
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

        

        // GET: PhoneRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();                          
            if (await _repository.GetByIdAsync(id.Value) is { } record){
                return View(record);
            }
            return NotFound();
        }
    }
}
