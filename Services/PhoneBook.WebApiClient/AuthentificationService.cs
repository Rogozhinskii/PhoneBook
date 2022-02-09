using Microsoft.AspNetCore.Identity;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly HttpClient _client;

        public AuthentificationService(HttpClient client)
        {
            _client = client;
        }

        public string AuthenticatedUserName {get; set;}

        public string AuthenticatedUserRole { get; set; }

        public async Task<IAuthentificationResult> Login(IUserLogin login,CancellationToken cancel= default)
        {
            return null; //todo сделать нормально
            //var responce =await _client.PostAsJsonAsync("login", login, cancel).ConfigureAwait(false);
            //if (responce.IsSuccessStatusCode)
            //{
            //    AuthenticatedUserName=login.UserName;
            //    await GetUserRoleAsync(login.UserName, cancel);
            //    return true;
            //}                
            //return false;            
        }


        public async Task<bool> Logout(CancellationToken cancel = default)
        {
            var result = await _client.GetAsync("logout", cancel).ConfigureAwait(false);
            AuthenticatedUserName=string.Empty;
            AuthenticatedUserRole=string.Empty;
            return result.IsSuccessStatusCode;
        }

        public async Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default) {
            var result=await _client.GetStringAsync($"getRole/{userName}", cancel).ConfigureAwait(false);
            AuthenticatedUserRole=result;
            return result;
        }

        public Task<IAuthentificationResult> RegisterUser(IUserLogin userModel, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAuthentificationResult> RegisterUser(UserInfo userModel, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
