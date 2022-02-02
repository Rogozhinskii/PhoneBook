using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using System.Net.Http;

namespace PhoneBook.WPF
{
    public static class AddHttpClient
    {
        public static IContainerRegistry AddClient(this IContainerRegistry container,IConfigurationRoot config)
        {
            var address = $"{config.GetSection("ClientHost").Value}{config.GetSection("PhoneRecordRepositoryAddress").Value}";
            HttpClient client = new();
            client.BaseAddress = new($"{config.GetSection("ClientHost").Value}{config.GetSection("PhoneRecordRepositoryAddress").Value}");
            return container.Register<HttpClient>();
        }
    }
}
