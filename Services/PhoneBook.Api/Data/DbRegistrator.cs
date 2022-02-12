using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.DAL.Context;

namespace PhoneBook.Api.Data
{
    public static class DbRegistrator
    {
        /// <summary>
        /// регистрирует контекст базы данных
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<PhoneBookDB>(options =>
            {
                var dbSection = configuration.GetSection("DataBase");
                var type = dbSection["Type"];
                var rr = configuration.GetConnectionString(type);
                switch (type)
                {
                    case "SQLite": options.UseSqlite(configuration.GetConnectionString(type), sqliteOptions =>
                    {
                        sqliteOptions.MigrationsAssembly("PhoneBook.DAL.SQLite");
                    }); break;
                    case "SQLServer":options.UseSqlServer(configuration.GetConnectionString(type), sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure()
                                 .MigrationsAssembly("PhoneBook.DAL.SqlServer");
                    }); break;
                }                
            })
            ;
    }
}
