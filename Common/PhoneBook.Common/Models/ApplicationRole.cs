using Microsoft.AspNetCore.Identity;
using System;

namespace PhoneBook.Common.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
