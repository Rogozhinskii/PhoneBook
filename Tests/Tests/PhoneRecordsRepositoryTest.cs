using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneBook.Common.RandomInfo;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Repository;
using PhoneBook.Entities;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class PhoneRecordsRepositoryTest    
    {        
        private IRepository<PhoneRecord> _repository;
        
        

        [TestInitialize]
        public void Setup()
        {                    
            _repository = ContextHelper.Repository;
        }

        [TestMethod]
        public void ItemsCountTest()
        {
            var itemsCount = _repository.GetCountAsync().Result;
            Assert.IsTrue(itemsCount>0);
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
            Assert.IsNotNull(page);
            Assert.IsTrue(page.Items.Count() > 0);
        }
    }
}
