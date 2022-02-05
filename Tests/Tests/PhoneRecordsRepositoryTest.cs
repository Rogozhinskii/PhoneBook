using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.Common.RandomInfo;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Repository;
using PhoneBook.Entities;
using PhoneBook.Interfaces;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PhoneRecordsRepositoryTest    
    {        
        private IRepository<PhoneRecord> _repository;

        private void Populate(PhoneBookDB context)
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

        private IRepository<PhoneRecord> GetInMemmoryRepository()
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

        [TestInitialize]
        public void Setup()
        {
            _repository = GetInMemmoryRepository();
        }

        [TestMethod]
        public void ItemsCountTest()
        {
            var itemsCount = _repository.GetCountAsync().Result;
            Assert.AreEqual(2, itemsCount);
        }

        [TestMethod]
        public void AddItemTest()
        {
            var item = new PhoneRecord
            {
                FirstName = RandomData.GetRandomName(),
                LastName = RandomData.GetRandomSurname(),
                Patronymic = RandomData.GetRandomPatronymic()
            };
            item = _repository.AddAsync(item).Result;
            Assert.AreNotEqual(0,item.Id);
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            var itemsCount=_repository.GetCountAsync().Result;            
            var result=_repository.DeleteByIdAsync(1).Result;
            var itemsCountAfterDelete = _repository.GetCountAsync().Result;
            Assert.AreNotEqual(itemsCount,itemsCountAfterDelete);
        }

        [TestMethod]
        [DataRow(0)]
        public void GetPageTest(int pageindex)
        {
            var page=_repository.GetPage(pageindex, _repository.GetCountAsync().Result).Result;            
            Assert.AreEqual(2,page.PageSize);
        }
    }
}
