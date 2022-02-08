using System.Collections.Generic;

namespace PhoneBook.Interfaces
{
    public interface IAuthentificationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public List<string> Errors { get; set; }
    }
}
