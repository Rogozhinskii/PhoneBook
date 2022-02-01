using Microsoft.AspNetCore.Identity;
using PhoneBook.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class AuthenticationService : IAuthentication
    {
        private readonly HttpClient _client;

        public AuthenticationService(HttpClient client)
        {
            _client = client;
        }
        public async Task<bool> Login(IUserLogin login,CancellationToken cancel= default)
        {
            var responce =await _client.PostAsJsonAsync("login", login, cancel);
            if (responce.IsSuccessStatusCode)
                return true;
            return false;
            
        }
    }
}
