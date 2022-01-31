using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Controllers.Base;
using PhoneBook.Common;
using PhoneBook.Common.Models;
using PhoneBook.Entities;
using PhoneBook.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    public class PhoneRecordRepositoryController : MappedEntityController<PhoneRecordInfo,PhoneRecord>
    {
        public PhoneRecordRepositoryController(IRepository<PhoneRecord> repository, IMapper mapper)
            : base(repository, mapper) { }
        

        public override async Task<ActionResult<IPage<PhoneRecordInfo>>> GetPage(string searchString, CancellationToken cancel = default)
        {
            var items = await _repository.WhereAsync(x => x.FirstName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                                                     || x.LastName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase), cancel);
            var result = new Page<PhoneRecordInfo> { Items = GetItem(items), TotalCount = items.Count(), PageIndex = 0, PageSize = items.Count() };
            return items.Any()
                   ? Ok(result)
                   : NotFound();
        }




    }
}
