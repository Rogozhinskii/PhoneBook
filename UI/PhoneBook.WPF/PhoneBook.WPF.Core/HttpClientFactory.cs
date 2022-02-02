using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace PhoneBook.WPF.Core
{
    public enum HttpClientType
    {
        RepositoryClient,
        AuthentificationClient
    }
    public class HttpClientFactory
    {
        private IConfigurationRoot _config;

        public HttpClientFactory() { }

        public HttpClientFactory(IConfigurationRoot config)
        {
            _config = config;
        }
        public HttpClient GetClient(HttpClientType clientType)
        {
            HttpClient client = new();            
            switch (clientType)
            {
                case HttpClientType.RepositoryClient:
                    client.BaseAddress = new($"{_config.GetSection("ClientHost").Value}{_config.GetSection("PhoneRecordRepositoryAddress").Value}");
                break;
                case HttpClientType.AuthentificationClient:
                    client.BaseAddress = new($"{_config.GetSection("ClientHost").Value}{_config.GetSection("AccountControllerAddress").Value}");
                break;
                    default: throw new InvalidOperationException("Invalig HttpClient Type");
            }
            
            return client;
        }
    }
}
