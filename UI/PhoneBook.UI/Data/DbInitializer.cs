using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Models;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly string _administratorDefaultPass = "Administrator123";

        public DbInitializer(PhoneBookDB db, UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeData()
        {
            if (_db.PhoneRecords.Any()) return;
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            await InitializeUsersAndRoles();
            var record = Enumerable.Range(1, 100).Select(i => new PhoneRecord
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

        private async Task InitializeUsersAndRoles()
        {
            var adminRole = new ApplicationRole
            {
                Name = UserRoles.Administrator,
                CreatedDate = System.DateTime.Now,
                Description = "Administrator role"
            };
            var userRole = new ApplicationRole
            {
                Name = UserRoles.RegularUser,
                CreatedDate = System.DateTime.Now,
                Description = "RegularUser role"
            };

            var roleResult1=await _roleManager.CreateAsync(adminRole).ConfigureAwait(false);
            var roleResult2=await _roleManager.CreateAsync(userRole).ConfigureAwait(false);

            var regularUsers = Enumerable.Range(1, 3).Select(i => new User
            {
                UserName = RandomData.GetRandomUserName(),
                Email = RandomData.GetRandomEmail()
            });

            foreach (User item in regularUsers)
            {
                var result=await _userManager.CreateAsync(item, RandomData.GetRandomPassword()).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(item, UserRoles.RegularUser).ConfigureAwait(false);
                }
            }

            var administrator = new User
            {
                UserName = "Administrator",
                Email = "Administrator@gmail.com"
            };
            var adminResult=await _userManager.CreateAsync(administrator, _administratorDefaultPass).ConfigureAwait(false);
            if (adminResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(administrator, UserRoles.Administrator).ConfigureAwait(false);
            }

        }
    }
}
