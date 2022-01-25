using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities;

namespace PhoneBook.DAL.Context
{
    public class PhoneBookDB:DbContext
    {
        public DbSet<PhoneRecord> PhoneRecords { get; set; }

        public PhoneBookDB(DbContextOptions<PhoneBookDB> options)
            : base(options) {}
    }
}
