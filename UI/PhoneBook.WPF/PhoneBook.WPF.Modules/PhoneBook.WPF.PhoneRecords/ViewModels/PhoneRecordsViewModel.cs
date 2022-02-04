using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core;
using PhoneBook.WPF.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PhoneBook.WPF.PhoneRecords.ViewModels
{
    public class PhoneRecordsViewModel:BindableBase
    {
        private readonly IPhoneRecordModel _phoneRecordModel;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand<object> _editRecordCommand;
        private DelegateCommand _addNewRecordCommand;
        private DelegateCommand _loadDataCommand;
        private DelegateCommand<object> _deleteRecordCommand;


        public PhoneRecordsViewModel(IPhoneRecordModel phoneRecordModel,IDialogService dialogService,IEventAggregator eventAggregator)
        {
            _phoneRecordModel = phoneRecordModel;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _recordsViewSource = new();
            _recordsViewSource.Source = _phoneRecordModel.PhoreRecords;
            _recordsViewSource.Filter += OnFiltered;
            _eventAggregator.GetEvent<FilterTextChanged>().Subscribe((text) =>
            {
                FilterText=text;
            });
                        
        }


        #region FilterText - поле для фильтрации
        private string _filterText;
        /// <summary>
        /// Поле для фильтрации
        /// </summary>
        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); _recordsViewSource.View.Refresh();  }
        }

        #endregion

        private void OnFiltered(object sender, FilterEventArgs e)
        {             
            if (!(e.Item is PhoneRecordInfo record) || string.IsNullOrEmpty(FilterText)) return;
            if (record.FirstName.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) return;
            if (record.LastName.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) return;
            e.Accepted = false;
        }

        public ICollectionView RecordsView => _recordsViewSource?.View;
        CollectionViewSource _recordsViewSource;


        


        public DelegateCommand LoadDataCommand =>
           _loadDataCommand ??= _loadDataCommand = new(async() =>
           {
               await _phoneRecordModel.LoadData();
           });

        
        /// <summary>
        /// Выполняет добавление новой записи
        /// </summary>
        public DelegateCommand AddNewRecordCommand =>
           _addNewRecordCommand ??= _addNewRecordCommand = new(() =>
           {
               if (_phoneRecordModel.IsUserCanAddNewRecord())
               {
                   var dialogParameter = new DialogParameters();
                   dialogParameter.Add(DialogNames.NewRecord, new PhoneRecordInfo());
                   _dialogService.Show(DialogNames.AddRecordDialog, dialogParameter,async r =>
                   {
                       if (r.Result == ButtonResult.Cancel || r.Result==ButtonResult.None) return;                       
                       await _phoneRecordModel.AddNewRecord(r.Parameters.GetValue<PhoneRecordInfo>(DialogNames.NewRecord));
                       _recordsViewSource.View.Refresh();
                       
                   });

               }
               else
               {
                   ShowNotification(DialogNames.Unauthorized);
               }
               
           });

        

        /// <summary>
        /// Выполняет удаление записи
        /// </summary>
        public DelegateCommand<object> DeleteRecordCommand =>
           _deleteRecordCommand ??= _deleteRecordCommand = new((obj) =>
           {
               if (!_phoneRecordModel.IsUserCanDeleteRecord())
               {
                   ShowNotification(DialogNames.Unauthorized);
                   return;
               }   
               var itemList = (obj as ObservableCollection<object>).Cast<PhoneRecordInfo>().ToList();
               var msg = $"Вы уверены, что хотите удалить {itemList.Count} записей?";
               var parameters = new DialogParameters{
                { DialogNames.DialogMessage, msg }
            };
               _dialogService.Show(DialogNames.NotificationDialog, parameters, async r =>
               {
                   if (r.Result != ButtonResult.OK) return;
                   foreach (var item in itemList)
                       await _phoneRecordModel.DeleteRecord(item);                   
                   _recordsViewSource?.View.Refresh();
               });

           });      


        /// <summary>
        /// Выполняет редактирование записи
        /// </summary>
        public DelegateCommand<object> EditRecordCommand =>
           _editRecordCommand ??= _editRecordCommand = new((obj) =>
           {
               if (_phoneRecordModel.IsUserCanEditRecord())
               {
                   var firstSelectedItem = (obj as ObservableCollection<object>).Cast<PhoneRecordInfo>().FirstOrDefault();
                   if (firstSelectedItem is null) return;
                   DialogParameters parameters = new()
                   {
                       { DialogNames.EditableRecord, firstSelectedItem }
                   };
                   _dialogService.Show(DialogNames.EditRecordDialog, parameters, async r =>
                   {
                       if (r.Result != ButtonResult.OK) return;
                       await _phoneRecordModel.UpdateRecord(firstSelectedItem);
                       _recordsViewSource.View.Refresh();
                   });
               }
               else
               {
                   ShowNotification(DialogNames.Unauthorized);
               }
           });        

        public void ShowNotification(string message)
        {
            var dialogParameter = new DialogParameters();
            dialogParameter.Add(DialogNames.DialogMessage,message);
            _dialogService.ShowDialog(DialogNames.NotificationDialog, dialogParameter, null);
        }



    }
}
