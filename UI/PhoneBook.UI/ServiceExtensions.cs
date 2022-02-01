using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook
{
    public static class ServiceExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, IConfiguration configuration, string controllerAddress)
            where IInterface : class
            where IClient : class, IInterface => services
            .AddHttpClient<IInterface, IClient>((host,client) =>
            {

                client.BaseAddress = new($"{configuration["WebAPI"]}{controllerAddress}");
            });
            
    }
}
