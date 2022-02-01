using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Common.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        
    }
}
