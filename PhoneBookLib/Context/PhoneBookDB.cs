using Microsoft.EntityFrameworkCore;
using PhoneBookLib.Entities;

namespace PhoneBookLib.Context
{
    public class PhoneBookDB:DbContext
    {
        public DbSet<PhoneRecord> PhoneRecords { get; set; }

        public PhoneBookDB(DbContextOptions<PhoneBookDB> options)
            : base(options) {}
    }
}
