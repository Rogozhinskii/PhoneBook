using PhoneBook.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Common.Models
{
    /// <summary>
    /// Модель для формы аутентификации
    /// </summary>
    public class UserLogin: IUserLogin
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
