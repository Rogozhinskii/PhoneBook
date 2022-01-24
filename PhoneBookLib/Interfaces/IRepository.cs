using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLib.Interfaces
{
    public interface IRepository<T> where T : class,IEntity, new()
    {
        
        Task<T> GetByIdAsync(int id,CancellationToken cancel=default);
        Task<T> AddAsync(T item,CancellationToken cancel=default);
        Task<T> UpdateAsync(T item,CancellationToken cancel=default);
        Task<T> DeleteAsync(T item,CancellationToken cancel=default);
        Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default);
        Task<int> GetCountAsync(CancellationToken cancel = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);
        Task<int> SaveChangesAsync(CancellationToken cancel = default);
        Task<IPage<T>> GetPage(int pageIndex,int pageSize,CancellationToken cancel=default);
        Task<IEnumerable<T>> WhereAsync(Func<T, bool> filterExpression, CancellationToken cancel = default);
    }
}
