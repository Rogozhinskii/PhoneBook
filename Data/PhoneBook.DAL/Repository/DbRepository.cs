using Microsoft.EntityFrameworkCore;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Entities.Base;
using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PhoneBook.Common;

namespace PhoneBook.DAL.Repository
{
    /// <summary>
    /// Реализует доступ к хранилищу данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly PhoneBookDB _db;
        protected DbSet<T> Set { get; }

        public bool AutoSaveChanges { get; set; } = true;
        public DbRepository(PhoneBookDB db)
        {
            _db = db;
            Set = _db.Set<T>();
        }
        protected virtual IQueryable<T> Items => Set;

        public async Task<bool> ExistAsync(int id, CancellationToken cancel = default) =>
            await Items.AnyAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        
        public async Task<bool> ExistAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            return await ExistAsync(item.Id, cancel).ConfigureAwait(false);
        }

       
        public async Task<int> GetCountAsync(CancellationToken cancel = default) =>
            await Items.CountAsync(cancel).ConfigureAwait(false);


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default) =>
            await Items.ToArrayAsync(cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default)
        {
            if (count <= 0) return Enumerable.Empty<T>();
            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> ordered => ordered,
                { } q => q.OrderBy(i => i.Id)
            };
            if (skip > 0)
                query = query.Skip(skip);
            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            await _db.AddAsync(item, cancel).ConfigureAwait(false);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancel = default) =>
            Items switch
            {
                DbSet<T> set => await set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false),
                { } items => await items.SingleOrDefaultAsync(i => i.Id == id, cancel).ConfigureAwait(false),
                _ => throw new InvalidOperationException("data source not defined")
            };



        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Update(item);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> DeleteAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (!(await ExistAsync(item,cancel)))
                return null;
            _db.Remove(item);
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);
            if (item is null)
                item=await Set.Select(i => new T { Id = i.Id })
                         .FirstOrDefaultAsync(i => i.Id == id, cancel)
                         .ConfigureAwait(false);
            if (item is null) return null;            
            return await DeleteAsync(item, cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> WhereAsync(Func<T, bool> filterExpression, CancellationToken cancel = default) =>
            await Task.Run(() => Items.Where(filterExpression),cancel).ConfigureAwait(false);

        public async Task<int> SaveChangesAsync(CancellationToken cancel = default)=>
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            if(pageSize < 0) return new Page<T> { Items = Enumerable.Empty<T>(), TotalCount = pageSize, PageIndex = pageIndex, PageSize = pageSize };
            var query = Items;
            var total_count = await query.CountAsync(cancel).ConfigureAwait(false);
            if(total_count==0)
                return new Page<T> { Items = Enumerable.Empty<T>(), TotalCount = total_count, PageIndex = pageIndex, PageSize = pageSize };
            if(query is not IOrderedQueryable<T>)
                query=query.OrderBy(i=>i.Id);
            if (pageIndex > 0)
                query = query.Skip(pageIndex * pageSize);
            query=query.Take(pageSize);
            var items = await query.ToArrayAsync(cancel).ConfigureAwait(false);
            return new Page<T> { Items = items, TotalCount = total_count, PageIndex = pageIndex, PageSize = pageSize };
        }

        public void ChangeSaveMode(bool autoSaveChanges) =>
            AutoSaveChanges= autoSaveChanges;

        public Task<IPage<T>> GetPage(string filterString, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
