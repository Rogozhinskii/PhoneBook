using PhoneBookLib.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookLib.Entities
{
    public class PhoneRecord:Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
