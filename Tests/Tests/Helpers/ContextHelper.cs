using Microsoft.EntityFrameworkCore;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Repository;
using PhoneBook.Entities;
using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Helpers
{
    public class ContextHelper
    {
        private static IRepository<PhoneRecord> _repository;
        public static IRepository<PhoneRecord> Repository
        {
            get
            {
                if(_repository is null)
                {
                    _repository=GetInMemmoryRepository();
                }
                return _repository;
            }
        }

        public static List<PhoneRecord> GetRecords()
        {
            return new List<PhoneRecord>()
            {
                new PhoneRecord
                {
                    FirstName = "Георгий",
                    LastName = "Гагин"
                },
                new PhoneRecord
                {
                    FirstName = "Яков",
                    LastName = "Абакшин"
                }
            };
        }

        private static void Populate(PhoneBookDB context)
        {
            var items = new List<PhoneRecord>()
            {
                new PhoneRecord
                {
                    FirstName = "Георгий",
                    LastName = "Гагин"
                },
                new PhoneRecord
                {
                    FirstName = "Яков",
                    LastName = "Абакшин"
                }
            };

            context.AddRange(items);

            context.SaveChanges();
        }

        private static IRepository<PhoneRecord> GetInMemmoryRepository()
        {
            var options = new DbContextOptionsBuilder<PhoneBookDB>()
                 .UseInMemoryDatabase(databaseName: "MockDB")
                 .Options;

            var initContext = new PhoneBookDB(options);
            initContext.Database.EnsureCreated();
            Populate(initContext);
            var repository = new DbRepository<PhoneRecord>(initContext);
            return repository;
        }
    }
}
