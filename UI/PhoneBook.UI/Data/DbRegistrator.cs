using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.DAL.Context;
using System;
using System.IO;

namespace PhoneBook.Data
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
                var cs = configuration.GetConnectionString("SQLite");
                var file = File.Exists(cs);
                options.UseSqlite(configuration.GetConnectionString("SQLite"), sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly("PhoneBook.DAL");
                });
            })
            ;
    }
}
