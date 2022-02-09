using PhoneBook.Api.Helpers;
using PhoneBook.Common.Models;
using PhoneBook.Domain;
using PhoneBook.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class ApiAuthentification : IAuthentificationService
    {
        private readonly HttpClient _client;

        public ApiAuthentification(HttpClient client)
        {
            _client = client;
        }
        public string AuthenticatedUserName => throw new System.NotImplementedException();

        public string AuthenticatedUserRole => throw new System.NotImplementedException();

        public Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IAuthentificationResult> Login(IUserLogin login, CancellationToken cancel = default)
        {
            var request = await _client.PostAsJsonAsync("login", login, cancel).ConfigureAwait(false);
            var response = await request.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<AuthentificationResult>().ConfigureAwait(false);

            return response;
        }

        public async Task<bool> Logout(CancellationToken cancel = default)
        {
            var request=await _client.GetAsync("logout",cancel).ConfigureAwait(false);
            return request.IsSuccessStatusCode;
        }

        public async Task<IAuthentificationResult> RegisterUser(UserInfo userModel, CancellationToken cancel = default)
        {
            var request = await _client.PostAsJsonAsync("registration", userModel, cancel).ConfigureAwait(false);
            var response = await request.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<AuthentificationResult>().ConfigureAwait(false);
            return response;
        }
    }
}
