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
using Prism.Services.Dialogs;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace PhoneBook.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IDialogService _dialogService;
        protected override Window CreateShell()
        {
            _dialogService = Container.Resolve<IDialogService>();
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// все не обработанные исключения будут идти сюда.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DialogParameters dialogParameters = new()
            {
                { DialogNames.DialogMessage, e.Exception.Message }
            };
            _dialogService.ShowDialog(DialogNames.NotificationDialog, dialogParameters, result => { });
            e.Handled = true;
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ConfigurationBuilder configurationBuilder = new();
            IConfigurationRoot config=configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                                                          .AddJsonFile("appsettings.json").Build();
            containerRegistry.RegisterInstance(config);
            HttpClientFactory clientFactory = new(config);
            var webRepository = new WebRepository<PhoneRecordInfo>(clientFactory.GetClient(HttpClientType.RepositoryClient));
            var authentificationService = new ApiAuthentification(clientFactory.GetClient(HttpClientType.AuthentificationClient));
            containerRegistry.RegisterInstance<IWebRepository<PhoneRecordInfo>>(webRepository);
            containerRegistry.RegisterInstance<IAuthentificationService>(authentificationService);
            containerRegistry.RegisterSingleton<ITokenHandler, TokenHandler>();
    


        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NotificationToolsModule>();
            moduleCatalog.AddModule<PhoneRecordsModule>();
        }


    }
}



