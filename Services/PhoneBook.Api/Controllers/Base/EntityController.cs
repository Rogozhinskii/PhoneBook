using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DAL.Entities.Base;
using PhoneBook.Interfaces;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController<T> : ControllerBase where T:Entity,new()
    {
        private readonly IRepository<T> _repository;

        public EntityController(IRepository<T> repository)=>
            _repository = repository;

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)=>
            await _repository.GetByIdAsync(id) is { } item
            ?Ok(item)
            :NotFound();
        
    }
}
