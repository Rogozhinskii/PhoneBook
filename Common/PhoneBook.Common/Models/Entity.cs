using PhoneBook.Interfaces;
using System;

namespace PhoneBook.Common.Models
{
    public class Entity<T> : IEntity<T>
    {
        public T Id {get;set;}
                
        public override string ToString()=>
            Id.ToString();
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return obj?.ToString()==ToString();
        }

    }
}
