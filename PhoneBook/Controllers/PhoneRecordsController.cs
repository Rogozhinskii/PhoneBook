using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookLib.Entities;
using PhoneBookLib.Interfaces;

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
        public async Task<IActionResult> Index(int? pageIndex,int pageSize=5)
        {
            var result = await _repository.GetPage(pageIndex??0, pageSize);
            return View(result);
            //return View(await _repository.GetAllAsync());
        }

        // GET: PhoneRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneRecord = await _repository.GetByIdAsync(id.Value);                
            if (phoneRecord == null)
            {
                return NotFound();
            }

            return View(phoneRecord);
        }

        // GET: PhoneRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Patronymic,PhoneNumber,Address,Description,Id")] PhoneRecord phoneRecord)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(phoneRecord);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneRecord);
        }

        // GET: PhoneRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneRecord = await _repository.GetByIdAsync(id.Value);
            if (phoneRecord == null)
            {
                return NotFound();
            }
            return View(phoneRecord);
        }

        // POST: PhoneRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Patronymic,PhoneNumber,Address,Description,Id")] PhoneRecord phoneRecord)
        {
            if (id != phoneRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(phoneRecord);
                    await _repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneRecordExists(phoneRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phoneRecord);
        }

        // GET: PhoneRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneRecord = await _repository.GetByIdAsync(id.Value);                
            if (phoneRecord == null)
            {
                return NotFound();
            }

            return View(phoneRecord);
        }

        // POST: PhoneRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var phoneRecord = await _repository.PhoneRecords.FindAsync(id);
            //_repository.PhoneRecords.Remove(phoneRecord);
            //await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneRecordExists(int id)
        {
            return false;// _repository.PhoneRecords.Any(e => e.Id == id);
        }

        //public async Task<IPage<PhoneRecord>> GetPage(int pageIndex,int pageSize)
        //{
        //    var result=await _repository.GetPage(pageIndex,pageSize);
        //}
    }
}
