using PhoneBook.DAL.Entities.Base;

namespace PhoneBook.Models
{
    public class PhoneRecordViewModel: Entity
    {       
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public string Patronymic { get; set; }       
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
