using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Windows.Data;

namespace PhoneBook.WPF.PhoneRecords.ViewModels
{
    public class PhoneRecordsViewModel:BindableBase
    {
        private readonly IPhoneRecordModel _phoneRecordModel;
        private readonly IAuthentication _authentication;
        

        public PhoneRecordsViewModel(IPhoneRecordModel phoneRecordModel, IAuthentication authentication)
        {
            _phoneRecordModel = phoneRecordModel;
            _authentication = authentication;
            

            

            _recordsViewSource = new();
            _recordsViewSource.Source = _phoneRecordModel.PhoreRecords;
            
        }


        public ICollectionView RecordsView => _recordsViewSource?.View;
        CollectionViewSource _recordsViewSource;


        private DelegateCommand _loadDataCommand;

        public DelegateCommand LoadDataCommand =>
           _loadDataCommand ??= _loadDataCommand = new(async() =>
           {
               await _phoneRecordModel.LoadData();
           });


        private DelegateCommand _getRoleCommand;

        public DelegateCommand GetRoleCommand =>
           _getRoleCommand ??= _getRoleCommand = new(async () =>
           {
               var user = new UserLogin
               {
                   UserName = "Administrator",
                   Password = "Ca3(PO4)2"
               };
               var role = await _authentication.GetUserRoleAsync(user.UserName);
           });



    }
}
