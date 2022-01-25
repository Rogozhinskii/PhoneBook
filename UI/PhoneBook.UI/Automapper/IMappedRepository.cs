using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Automapper
{
    public interface IMappedRepository<T, TBase>
        where T : IEntity, new()
        where TBase : IEntity, new()
    {
        Task<T> GetById(int id);
        Task<IPage<T>> GetPage(Func<TBase, bool> filterExpression, CancellationToken cancel = default);
        Task<IPage<T>> GetPage(int pageIndex, int pageSize);
    }
}