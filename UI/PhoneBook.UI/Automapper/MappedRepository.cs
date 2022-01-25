using AutoMapper;
using PhoneBook.DAL.Repository;
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

        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => _mapper.Map<IEnumerable<T>>(items);



        protected IPage<T> GetItem(IPage<TBase> page)
        {
            return new Page<T>(GetItem(page.Items), page.TotalCount, page.PageIndex, page.PageSize);
        }

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return GetItem(result);
        }

        public async Task<IPage<T>> GetPage(Func<TBase, bool> filterExpression, CancellationToken cancel = default)
        {
            var items = await _repository.WhereAsync(filterExpression, cancel).ConfigureAwait(false);
            return new Page<T>(GetItem(items), items.Count(), 0, items.Count());
        }

        public async Task<T> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return GetItem(item);

        }


    }
}
