using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
