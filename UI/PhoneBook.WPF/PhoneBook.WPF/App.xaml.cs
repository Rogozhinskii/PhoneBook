using Microsoft.Extensions.Configuration;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WebApiClient;
using PhoneBook.WPF.Core;
using PhoneBook.WPF.NotificationTools;
using PhoneBook.WPF.PhoneRecords;
using PhoneBook.WPF.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.IO;
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
            IConfigurationRoot config=configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                                                          .AddJsonFile("appsettings.json").Build();
            containerRegistry.RegisterInstance(config);
            HttpClientFactory clientFactory = new(config);
            var webRepository = new WebRepository<PhoneRecordInfo>(clientFactory.GetClient(HttpClientType.RepositoryClient));
            var authentificationService = new AuthentificationService(clientFactory.GetClient(HttpClientType.AuthentificationClient));
            containerRegistry.RegisterInstance<IRepository<PhoneRecordInfo>>(webRepository);
            containerRegistry.RegisterInstance<IAuthentificationService>(authentificationService);
    


        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NotificationToolsModule>();
            moduleCatalog.AddModule<PhoneRecordsModule>();
        }


    }
}



