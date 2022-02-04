using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;

namespace PhoneBook.Controllers
{
    /// <summary>
    /// Контроллер для выполнений crud над записями телефонной книги 
    /// </summary>
    public class PhoneRecordsController : Controller
    {
        private readonly IRepository<PhoneRecordInfo> _repository;

        //private readonly IMappedRepository<PhoneRecordViewModel, PhoneRecord> _repository;
        private readonly ILogger _logger;

        public PhoneRecordsController(IRepository<PhoneRecordInfo> repository,
                                      ILogger<PhoneRecordsController> logger)
        {
            _repository = repository;            
            _logger = logger;            
        }

        /// <summary>
        /// Главное представление вызвращает страничное представление Index
        /// </summary>
        /// <param name="currentFilter">текущий, установленный на странице фильтр по имени или фамилии</param>
        /// <param name="searchString">строка для поиска</param>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">размер страницы</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageIndex,int? pageSize)
        {
            
            if (searchString != null){
                pageIndex = 0;
                ViewData["CurrentFilter"] = searchString;
                _logger.LogInformation($"Redirect to {nameof(PhoneRecordsController)} index page.Filter text:{searchString}");
                return View(await _repository.GetPage(searchString));
            }
            else{
                searchString = currentFilter;
            }
            ViewData["pageSize"] = pageSize;
            ViewData["CurrentFilter"] = searchString;
            _logger.LogInformation($"Redirect to {nameof(PhoneRecordsController)} index page.Filter text:{searchString}");
            return View(await _repository.GetPage(pageIndex ?? 0, pageSize??5));            
        }

        /// <summary>
        /// Get метод представления предпросмсотра удаляемой записи
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {            
            if (id is null) return NotFound();
            return View(await _repository.GetByIdAsync(id.Value));
        }

        /// <summary>
        /// Выполняет удаление записи по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($">>>Start deleting record. Record id is :{id}");
            if (await _repository.DeleteByIdAsync(id) is null) return NotFound();
            _logger.LogInformation($">>>Record deleted. Record id is :{id}");
            TempData["SuccessMessage"] = $"Record deleted";
            return Redirect("~/");
        }
        
        /// <summary>
        /// Get метод, вызывает представление создание новой записи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles =UserRoles.Administrator+","+UserRoles.RegularUser)]
        public IActionResult Create()
        {
            _logger.LogInformation($"{nameof(PhoneRecordsController)}. Start creating a new record");
            return View();
        }

        /// <summary>
        /// Post метод, выполняет сохранение новой записи
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(nameof(Create))]
        [Authorize(Roles = UserRoles.Administrator + "," + UserRoles.RegularUser)]
        public async Task<IActionResult> Create(PhoneRecordInfo record)
        {
            if (record is null) return NotFound();
            _logger.LogInformation($">>>Start creating a new record.");
            var result=await _repository.AddAsync(record);
            _logger.LogInformation($">>>New record created. Record id is {result.Id}");           
            TempData["SuccessMessage"] = $"Record created";
            return Redirect("~/");
        }

        /// <summary>
        /// Вызывает представление страницы редактирования записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null) return NotFound();
            return View(await _repository.GetByIdAsync(id.Value));
        }

        /// <summary>
        /// Выполняет сохранение редактированной записи
        /// </summary>
        /// <param name="phoneRecord"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(nameof(Edit))]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> Edit(PhoneRecordInfo phoneRecord) =>
            await _repository.UpdateAsync(phoneRecord) is { } record
            ? Redirect("~/")
            : NotFound();

        /// <summary>
        /// Вызывает представление полной информации о записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null){
                _logger.LogError($"{nameof(PhoneRecordsController)}/Details passed id is null.");
                return NotFound();
            }
            if (await _repository.GetByIdAsync(id.Value) is { } record){
                _logger.LogInformation($">>>output of complete information about the record id:{id}");
                return View(record);
            }
            return NotFound();
        }

        
    }
}
