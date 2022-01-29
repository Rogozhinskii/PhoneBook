using PhoneBook.DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    /// <summary>
    /// Модель представления записи телефонного справочника
    /// </summary>
    public class PhoneRecordViewModel: Entity
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
    }
}
