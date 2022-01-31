using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common;
using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedEntityController<T,TBase> : ControllerBase
        where T : IEntity,new()
        where TBase : IEntity,new()
    {
        protected readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        public MappedEntityController(IRepository<TBase> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Конвертирует объект T в TBase
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);

        /// <summary>
        /// Конвертирует объект TBase в T
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);

        /// <summary>
        /// Конвертирует перечисление объектов T в перечисление объектов TBase
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _mapper.Map<IEnumerable<TBase>>(items);

        /// <summary>
        /// Конвертирует перечисление объектов TBase в перечисление объектов T 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => _mapper.Map<IEnumerable<T>>(items);

        protected IPage<T> GetItem(IPage<TBase> page) =>
           new Page<T>(GetItem(page.Items), page.TotalCount, page.PageIndex, page.PageSize);


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id) =>
            await _repository.GetByIdAsync(id) is { } item
            ? Ok(GetItem(item))
            : NotFound();

        [HttpGet("page[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IPage<T>>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var result = await _repository.GetPage(pageIndex, pageSize,cancel);
            return result.Items.Any()
                               ? Ok(GetItem(result))
                               :NotFound();
        }

        [HttpGet("{searchString}")]
        public virtual async Task<ActionResult<IPage<T>>> GetPage(string searchString, CancellationToken cancel = default)
        {   
           return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            if(await _repository.DeleteByIdAsync(id) is not { } item)
                return NotFound();
            return Ok(GetItem(item));
          
        }

        [HttpPost("addnew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAsync(T item)
        {
            var result = await _repository.AddAsync(GetBase(item));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(T item)
        {
            var result=await _repository.UpdateAsync(GetBase(item));
            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get),new {id=result.Id }, GetItem(result));
        }

        [HttpGet("count")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemsCount()=>
            Ok(await _repository.GetCountAsync());

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(int))]
        public async Task<IActionResult> GetAllItems() =>
            Ok(GetItem(await _repository.GetAllAsync()));

    }
}
