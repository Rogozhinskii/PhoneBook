using PhoneBookLib.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLib
{
    public class DbRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public DbRepository()
        {

        }
        public IQueryable<T> Items => throw new System.NotImplementedException();

        public Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetAsync(int id, CancellationToken cancel = default)
        {

            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(T item, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
