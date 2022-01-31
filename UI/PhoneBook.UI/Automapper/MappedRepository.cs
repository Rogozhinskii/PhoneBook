using AutoMapper;
using PhoneBook.Common;
using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Automapper
{
    public class MappedRepository<T, TBase> : IMappedRepository<T, TBase> where TBase : IEntity, new()
        where T : IEntity, new()
    {
        private readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        public MappedRepository(IRepository<TBase> repository, IMapper mapper)
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

        protected IPage<T> GetItem(IPage<TBase> page)=>
            new Page<T> { Items = GetItem(page.Items), TotalCount = page.TotalCount, PageIndex = page.PageIndex, PageSize = page.PageSize };
        

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return GetItem(result);
        }

        public async Task<IPage<T>> GetPage(Func<TBase, bool> filterExpression, CancellationToken cancel = default)
        {
            var items = await _repository.WhereAsync(filterExpression, cancel).ConfigureAwait(false);
            return new Page<T> { Items = GetItem(items), TotalCount = items.Count(), PageIndex = 0, PageSize = items.Count() };
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancel = default)
        {
            var item = await _repository.GetByIdAsync(id,cancel);
            return GetItem(item);
        }

        public async Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default) =>
             GetItem(await _repository.DeleteByIdAsync(id,cancel));

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            var result=await _repository.AddAsync(GetBase(item),cancel);
            return GetItem(result);
        }

        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            var result = await _repository.UpdateAsync(GetBase(item), cancel);           
            return GetItem(result);            
        }
    }
}
