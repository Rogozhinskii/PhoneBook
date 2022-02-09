using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    public interface IWebRepository<T> where T : IEntity,new()
    {
        public Task<T> DeleteByIdAsync(int id, string token, CancellationToken cancel = default);
        public Task<T> AddAsync(T item, string token, CancellationToken cancel = default);
        public Task<T> UpdateAsync(T item, string token, CancellationToken cancel = default);
    }
}
