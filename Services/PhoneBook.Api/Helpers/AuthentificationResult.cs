using PhoneBook.Interfaces;
using System.Collections.Generic;

namespace PhoneBook.Api.Helpers
{
    public class AuthentificationResult : IAuthentificationResult
    {
        public string Token {get;set;}
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
