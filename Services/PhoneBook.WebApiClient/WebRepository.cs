using Microsoft.AspNetCore.Mvc;
using PhoneBook.Common;
using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class WebRepository<T>: IWebRepository<T> where T:IEntity,new()
    {
        private readonly HttpClient _client;

       
        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> DeleteByIdAsync(int id, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await DeleteByIdAsync(id, cancel).ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T item, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await AddAsync(item, cancel).ConfigureAwait(false);
        }

        public async Task<T> UpdateAsync(T item, string token, CancellationToken cancel = default)
        {
            SetToken(token);
            return await UpdateAsync(item, cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<T>($"{id}", cancel).ConfigureAwait(false);

        public bool SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }


        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            var responce = await _client.PostAsJsonAsync("addnew", item, cancel).ConfigureAwait(false);
            var result = await responce.EnsureSuccessStatusCode()
                                     .Content.ReadFromJsonAsync<T>(cancellationToken: cancel)
                                     .ConfigureAwait(false);
            return result;
        }
                    

        public async Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default)
        {
            var responce = await _client.DeleteAsync($"{id}", cancel).ConfigureAwait(false);
            return await responce.EnsureSuccessStatusCode()
                                .Content
                                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                                .ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            var responce = await _client.GetAsync($"page[{pageIndex}/{pageSize}]", cancel).ConfigureAwait(false);
            if (responce.StatusCode == HttpStatusCode.BadRequest)
            {
                return new Page<T>
                {
                    Items = Enumerable.Empty<T>(),
                    PageIndex = 0,
                    PageSize = 0,
                    TotalCount = 0
                };
            }
            return await responce.EnsureSuccessStatusCode()
                                 .Content
                                 .ReadFromJsonAsync<Page<T>>()
                                 .ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPage(string filterString, CancellationToken cancel = default)
        {
            var responce = await _client.GetAsync($"{filterString}", cancel).ConfigureAwait(false);
            if (responce.StatusCode == HttpStatusCode.NotFound)
            {
                return new Page<T>
                {
                    Items = Enumerable.Empty<T>(),
                    PageIndex = 0,
                    PageSize = 0,
                    TotalCount = 0
                };
            }
            return await responce.EnsureSuccessStatusCode()
                                    .Content.ReadFromJsonAsync<Page<T>>()
                                    .ConfigureAwait(false);
        }


        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            var responce = await _client.PutAsJsonAsync("update", item, cancel).ConfigureAwait(false);
            return await responce.EnsureSuccessStatusCode()
                                       .Content.ReadFromJsonAsync<T>()
                                       .ConfigureAwait(false);
        }


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default) =>
            await _client.GetFromJsonAsync<IEnumerable<T>>("getAll").ConfigureAwait(false);



        public async Task<T> DeleteAsync(T item, CancellationToken cancel = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "")
            {
                Content = JsonContent.Create(item)
            };
            var responce = await _client.SendAsync(request, cancel).ConfigureAwait(false);
            if (responce.StatusCode == HttpStatusCode.BadRequest)
                return default;
            return await responce.EnsureSuccessStatusCode()
                                  .Content.ReadFromJsonAsync<T>(cancellationToken: cancel)
                                  .ConfigureAwait(false);

        }

    }
}
