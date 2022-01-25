using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.RandomInfo;
using PhoneBook.DAL.Context;
using PhoneBook.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    /// <summary>
    /// если нужно будет наполнить БД случайными сущностями
    /// </summary>
    public class DbInitializer
    {
        private readonly PhoneBookDB _db;

        public DbInitializer(PhoneBookDB db)
        {
            _db = db;
        }

        public async Task InitializeData()
        {
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await _db.Database.MigrateAsync();
            var record = Enumerable.Range(1, 50).Select(i => new PhoneRecord
            {
                FirstName = RandomData.GetRandomName(),
                LastName = RandomData.GetRandomSurname(),
                PhoneNumber = RandomData.GetRandomPhoneNumber(),
                Patronymic = RandomData.GetRandomPatronymic(),
                Address = RandomData.GetRandomAddress(),
                Description = RandomData.GetRandomDescription(),
            }).ToArray();

            await _db.AddRangeAsync(record).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
