using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Common.Models;
using PhoneBook.DAL.Context;
using PhoneBook.Interfaces;
using PhoneBook.WebApiClient;
using PhoneBook.WPF.NotificationTools;
using PhoneBook.WPF.PhoneRecords;
using PhoneBook.WPF.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows;

namespace PhoneBook.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).AddJsonFile("appsettings.json");
            DbContextOptionsBuilder optionsBuilder = new();
            var config = configurationBuilder.Build();            
            HttpClient client = new();
            client.BaseAddress = new($"{config.GetSection("ClientHost").Value}{config.GetSection("PhoneRecordRepositoryAddress").Value}");
            var webClient=new WebRepository<PhoneRecordInfo>(client);
            containerRegistry.RegisterInstance<IRepository<PhoneRecordInfo>>(webClient);


            HttpClient authClient = new();
            authClient.BaseAddress = new($"{config.GetSection("ClientHost").Value}{config.GetSection("AccountControllerAddress").Value}");
            var authService = new AuthenticationService(authClient);
            containerRegistry.RegisterInstance<IAuthentication>(authService);


            //containerRegistry.RegisterInstance<PhoneBookDB>(ConfigureDbContext(config));




        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NotificationToolsModule>();
            moduleCatalog.AddModule<PhoneRecordsModule>();
        }


        private PhoneBookDB ConfigureDbContext(IConfigurationRoot config)
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).AddJsonFile("appsettings.json");
            DbContextOptionsBuilder<PhoneBookDB> optionsBuilder = new();            
            var section = config.GetSection("Database:Type");
            var type = section.Value;
            switch (type)
            {
                case "SQLite":
                    optionsBuilder.UseSqlite(config.GetConnectionString(type), sqliteOptions =>
                    {
                        sqliteOptions.MigrationsAssembly("PhoneBook.DAL.SQLite");
                    }); break;
                case "SQLServer":
                    optionsBuilder.UseSqlServer(config.GetConnectionString(type), sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure()
                                 .MigrationsAssembly("PhoneBook.DAL.SqlServer");
                    }); break;
                default: throw new InvalidOperationException("Invalid DB Type"); break;
            }

            return new PhoneBookDB(optionsBuilder.Options);

        }
    }
}





//containerRegistry.RegisterScoped<PhoneBookDB>(options =>
//{
//    ConfigurationBuilder configurationBuilder = new();
//    configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).AddJsonFile("appsettings.json");
//    DbContextOptionsBuilder optionsBuilder = new();
//    var config = configurationBuilder.Build();
//    var section = config.GetSection("Database:Type");
//    var type = section.Value;
//    switch (type)
//    {
//        case "SQLite":
//            options.UseSqlite(config.GetConnectionString(type), sqliteOptions =>
//            {
//                sqliteOptions.MigrationsAssembly("PhoneBook.DAL.SQLite");
//            }); break;
//        case "SQLServer":
//            optionsBuilder.UseSqlServer(config.GetConnectionString(type), sqlOptions =>
//            {
//                sqlOptions.EnableRetryOnFailure()
//                         .MigrationsAssembly("PhoneBook.DAL.SqlServer");
//            }); break;
//        default: throw new InvalidOperationException("Invalid DB Type"); break;
//    }
//});
