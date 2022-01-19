using Microsoft.EntityFrameworkCore;
using PhoneBookLib.Entities;

namespace PhoneBookLib.Context
{
    public class PhoneBookDB:DbContext
    {
        DbSet<PhoneRecord> PhoneRecords { get; set; }

        public PhoneBookDB(DbContextOptions<PhoneBookDB> options)
            : base(options) {}
    }
}
