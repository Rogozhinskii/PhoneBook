using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLib.Interfaces
{
    public interface IRepository<T> where T : class,IEntity, new()
    {
        IQueryable<T> Items { get; }
        Task<T> GetAsync(int id,CancellationToken cancel=default);
        Task<T> AddAsync(T item,CancellationToken cancel=default);
        Task<T> UpdateAsync(T item,CancellationToken cancel=default);
        Task RemoveAsync(T item,CancellationToken cancel=default);
    }
}
