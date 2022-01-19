using Microsoft.EntityFrameworkCore;
using PhoneBookLib.Context;
using PhoneBookLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLib
{
    public class DbRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly PhoneBookDB _db;
        protected DbSet<T> Set { get; }

        public DbRepository(PhoneBookDB db)
        {
            _db = db;            
            Set=_db.Set<T>();
        }
        protected virtual IQueryable<T> Items => Set;

        public async Task<bool> Exist(int id,CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == id,cancel).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T item,CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            return await Items.AnyAsync(i => i.Id ==item.Id, cancel).ConfigureAwait(false);
        }

        public async Task<int> GetCount(CancellationToken cancel=default)
        {
            return await Items.CountAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default)=>
            await Items.ToArrayAsync(cancel).ConfigureAwait(false);
        

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

        public Task<int> SaveChangesAsync(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
