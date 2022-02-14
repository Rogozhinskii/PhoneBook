using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Common.Models
{
    /// <summary>
    /// Модель представления записи телефонного справочника
    /// </summary>
    public class PhoneRecordInfo: Entity<Guid>,IEquatable<PhoneRecordInfo>
    {        

        [DisplayName("First Name")]
        [Required(ErrorMessage = "This Field is Required")]
        public string FirstName { get; set; }
        
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This Field is Required")]
        public string LastName { get; set; }

        [DisplayName("Patronymic")]
        public string Patronymic { get; set; }  
                
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "This Field is Required")]
        public string PhoneNumber { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public bool Equals(PhoneRecordInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && FirstName==other.FirstName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PhoneRecordInfo)obj);
        }
        public override int GetHashCode()=>
            Id.GetHashCode();
            
        
    }
}
