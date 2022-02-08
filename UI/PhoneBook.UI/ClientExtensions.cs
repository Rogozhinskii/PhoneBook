using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Interfaces;
using System.Threading.Tasks;

namespace PhoneBook
{
    public static class ClientExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address, IConfiguration configuration,ISession session= default)
            where IInterface : class where IClient : class, IInterface => services
            .AddHttpClient<IInterface, IClient>((host, client) =>
            {
                client.BaseAddress = new($"{configuration["WebApi"]}{address}");
                var tt = session.GetString("Role");
            })
            ;
    }
}
