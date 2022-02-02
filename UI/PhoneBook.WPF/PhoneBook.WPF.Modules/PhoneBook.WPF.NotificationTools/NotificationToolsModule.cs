using PhoneBook.WPF.Core;
using PhoneBook.WPF.Core.Interfaces;
using PhoneBook.WPF.NotificationTools.Controls;
using PhoneBook.WPF.NotificationTools.Model;
using PhoneBook.WPF.NotificationTools.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace PhoneBook.WPF.NotificationTools
{
    public class NotificationToolsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NotificationToolsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Header, typeof(HeaderControl));
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<HeaderControl, HeaderControlViewModel>();
            containerRegistry.RegisterDialog<AuthentificationDialog, AuthentificationDialogViewModel>();
            containerRegistry.Register<IAuthentificationModel, AuthentificationModel>();
            
        }
    }
}
