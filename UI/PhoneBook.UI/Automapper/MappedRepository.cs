using AutoMapper;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.DAL.Repository
{
    internal abstract class MappedRepository<T, TBase> 
        where TBase : IEntity, new()
        where T : IEntity,new()
    {
        private readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        public MappedRepository(IRepository<TBase> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => _mapper.Map<IEnumerable<T>>(items);



        protected IPage<T> GetPage(IPage<TBase> page)
        {
            return new Page<T>(GetItem(page.Items), page.TotalCount, page.PageIndex, page.PageSize);          
        }

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return GetPage(result);
        }

        public async Task<T> GetById(int id)
        {
            var item=await _repository.GetByIdAsync(id);
            return GetItem(item);
          
        }
          

    }
}
