using PhoneBook.Interfaces;
using System;

namespace PhoneBook.Common.Models
{
    public class Entity<T> : IEntity<T>
    {
        public T Id {get;set;}
       
    }
}
