using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Common.Models;
using PhoneBook.Entities;

namespace PhoneBook.DAL.Context
{
    /// <summary>
    /// Контекст подключения к БД
    /// </summary>
    public class PhoneBookDB:IdentityDbContext<User,ApplicationRole, string>
    {
        /// <summary>
        /// Таблица справочника
        /// </summary>
        public DbSet<PhoneRecord> PhoneRecords { get; set; }

        public PhoneBookDB(DbContextOptions<PhoneBookDB> options)
            : base(options) {}
    }
}
