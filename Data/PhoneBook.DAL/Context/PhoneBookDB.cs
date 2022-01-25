using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities;

namespace PhoneBook.DAL.Context
{
    /// <summary>
    /// Контекст подключения к БД
    /// </summary>
    public class PhoneBookDB:DbContext
    {
        /// <summary>
        /// Таблица справочника
        /// </summary>
        public DbSet<PhoneRecord> PhoneRecords { get; set; }

        public PhoneBookDB(DbContextOptions<PhoneBookDB> options)
            : base(options) {}
    }
}
