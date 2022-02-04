using Microsoft.AspNetCore.Identity;
using System;

namespace PhoneBook.Common.Models
{
    /// <summary>
    /// Роль пользователя приложения
    /// </summary>
    public class ApplicationRole:IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
