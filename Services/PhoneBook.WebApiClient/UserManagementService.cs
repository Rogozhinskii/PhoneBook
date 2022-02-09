using Microsoft.AspNetCore.Identity;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class UserManagementService : IUserManagementService
    {
        private readonly HttpClient _client;

        public UserManagementService(HttpClient client)=>
            _client = client;
        public bool SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
        public async Task<IEnumerable<IdentityRole>> GetRoles(string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await _client.GetFromJsonAsync<IEnumerable<ApplicationRole>>("getRoles", cancel).ConfigureAwait(false);
        }

        public async Task<IdentityRole> GetRoleById(string id, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await _client.GetFromJsonAsync<ApplicationRole>($"getRole/{id}", cancel).ConfigureAwait(false);
            
        }

        public async Task<bool> UpdateRole(IdentityRole role, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var responce = await _client.PostAsJsonAsync("updateRole", role, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                     .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                     .ConfigureAwait(false);
            return result;            
        }

        public async Task<bool> CreateRole(IdentityRole role, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var responce = await _client.PostAsJsonAsync("createRole", role, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                     .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                     .ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteRole(IdentityRole role, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var request = new HttpRequestMessage(HttpMethod.Delete, "deleteRole")
            {
                Content = JsonContent.Create(role)
            };
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;
            var result = await response
                            .EnsureSuccessStatusCode()
                            .Content
                            .ReadFromJsonAsync<bool>(cancellationToken: cancel)
                            .ConfigureAwait(false);
            return result;

        }

        #region Actions with users
        public async Task<IEnumerable<IdentityUser>> GetAllUsers(string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await _client.GetFromJsonAsync<IEnumerable<User>>("getUsers", cancel).ConfigureAwait(false);
        }

        public async Task<bool> CreateUser(IUserLogin user, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var userInfo = (UserInfo)user;
            var responce = await _client.PostAsJsonAsync("addNewUser", userInfo, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                     .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                     .ConfigureAwait(false);
            return result;
        }

        

        public async Task<IdentityUser> GetUserById(string id, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await _client.GetFromJsonAsync<IdentityUser>($"getUser/{id}", cancel).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetUserRoles(string userId,string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await _client.GetFromJsonAsync<IList<string>>($"getUserRoles/{userId}", cancel).ConfigureAwait(false);

        }

        public async Task<bool> UpdateUser(IdentityUser user, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var responce = await _client.PostAsJsonAsync("updateUser", user, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                     .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                     .ConfigureAwait(false);

            return result;
        }

        public async Task<bool> RemoveFromRole(IdentityUser user, string existingRole, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var responce = await _client.PostAsJsonAsync($"removeFromRole/{existingRole}", user, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                    .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                    .ConfigureAwait(false);
            return result;
        }

        public async Task<bool> AddToRole(IdentityUser user, string newRole, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            var responce = await _client.PostAsJsonAsync($"addToRole/{newRole}", user, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                    .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel)
                                    .ConfigureAwait(false);
            return result;
        }

        public async Task<string> GetRoleIdByName(string roleName, string token, CancellationToken cancel = default)
        {
            SetToken(token);            
            return await _client.GetStringAsync($"getRoleId/{roleName}",cancel).ConfigureAwait(false);
        }
        #endregion


    }
}
