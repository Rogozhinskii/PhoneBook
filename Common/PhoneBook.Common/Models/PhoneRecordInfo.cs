using PhoneBook.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Common.Models
{
    /// <summary>
    /// Модель представления записи телефонного справочника
    /// </summary>
    public class PhoneRecordInfo: IEntity
    {
        public Guid Id { get; set; }

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
        
    }
}
