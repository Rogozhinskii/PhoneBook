using PhoneBook.WPF.Core;
using PhoneBook.WPF.NotificationTools.Controls;
using Prism.Ioc;
using Prism.Modularity;
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
            
        }
    }
}
