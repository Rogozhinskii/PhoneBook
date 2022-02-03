using PhoneBook.WPF.Core;
using PhoneBook.WPF.PhoneRecords.Models;
using PhoneBook.WPF.PhoneRecords.ViewModels;
using PhoneBook.WPF.PhoneRecords.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PhoneBook.WPF.PhoneRecords
{
    public class PhoneRecordsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public PhoneRecordsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }


        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(PhoneRecordsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPhoneRecordModel, PhoneRecordsModel>();
            containerRegistry.RegisterDialog<AddRecordDialog, AddRecordDialogViewModel>();
            containerRegistry.RegisterDialog<EditRecordDialog, EditRecordDialogViewModel>();
        }
    }
}
