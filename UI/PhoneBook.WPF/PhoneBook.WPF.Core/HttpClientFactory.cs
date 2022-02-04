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

    /// <summary>
    /// Фабрика слиентов для api
    /// </summary>
    public class HttpClientFactory
    {
        private IConfigurationRoot _config;

        public HttpClientFactory() { }

        public HttpClientFactory(IConfigurationRoot config)
        {
            _config = config;
        }

        /// <summary>
        /// Возвращает сконфигурированный http слиент
        /// </summary>
        /// <param name="clientType">тип клиента</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
