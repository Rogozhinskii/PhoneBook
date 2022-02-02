﻿using Microsoft.AspNetCore.Identity;
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

        public string AuthenticatedUserName {get; set;}

        public string AuthenticatedUserRole { get; set; }

        public async Task<bool> Login(IUserLogin login,CancellationToken cancel= default)
        {
            var responce =await _client.PostAsJsonAsync("login", login, cancel).ConfigureAwait(false);
            if (responce.IsSuccessStatusCode)
            {
                AuthenticatedUserName=login.UserName;
                return true;
            }                
            return false;            
        }
        
        public async Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default)=>
            await _client.GetStringAsync($"getRole/{userName}",cancel).ConfigureAwait(false);
           

    }
}
