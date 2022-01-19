using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookLib.Context;
using System;

namespace PhoneBook.Data
{
    public static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<PhoneBookDB>(options =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case "MSSQL":
                        options.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "SQLite":
                        options.UseSqlite(configuration.GetConnectionString(type));
                        break;                  
                    case null: throw new InvalidOperationException();
                    default: throw new InvalidOperationException();
                }
            })
            ;
    }
}
