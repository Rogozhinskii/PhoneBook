using PhoneBook.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Common.Models
{
    public class UserInfo: IUserLogin
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is Required"), MinLength(6, ErrorMessage = "Need more than 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
    }
}
