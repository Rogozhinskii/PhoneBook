using PhoneBook.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class PermissionService : IPermissionService
    {
        private readonly HttpClient _client;

        public PermissionService(HttpClient client)=>
            _client = client;

        public async Task<bool> CanCreate(string token, CancellationToken cancelationToken = default)
        {
            SetToken(token);
            var responce = await _client.GetAsync("cancreate", cancelationToken).ConfigureAwait(false);
            if (responce.StatusCode == HttpStatusCode.Unauthorized || responce.StatusCode == HttpStatusCode.NotFound)
                return false;
            return true;
        }

        public async Task<bool> CanDelete(string token, CancellationToken cancelationToken = default)
        {
            SetToken(token);
            var responce = await _client.GetAsync("candelete", cancelationToken).ConfigureAwait(false);
            if (responce.StatusCode == HttpStatusCode.Unauthorized || responce.StatusCode == HttpStatusCode.NotFound)
                return false;
            return true;
        }

        public async Task<bool> CanEdit(string token,CancellationToken cancelationToken = default)
        {
            SetToken(token);
            var responce=await _client.GetAsync("canedit", cancelationToken).ConfigureAwait(false);
            if(responce.StatusCode==HttpStatusCode.Unauthorized || responce.StatusCode==HttpStatusCode.NotFound)
                return false;
            return true;
        }

        public bool SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}
