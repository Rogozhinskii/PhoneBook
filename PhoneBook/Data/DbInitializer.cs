using PhoneBookLib.Context;
using PhoneBookLib.Entities;
using PhoneBookLib.RandomInfo;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    public class DbInitializer
    {
        private readonly PhoneBookDB _db;

        public DbInitializer(PhoneBookDB db)
        {
            _db = db;
        }

        public async Task InitializeData()
        {
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
