using PhoneBook.Interfaces;
using System;

namespace PhoneBook.DAL.Entities.Base
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
