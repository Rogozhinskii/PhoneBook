using PhoneBook.Common.Models;
using PhoneBook.WPF.Core;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace PhoneBook.WPF.PhoneRecords.ViewModels
{
    internal class AddRecordDialogViewModel:DialogViewModel
    {
        private DelegateCommand _cancelCommand;
        private DelegateCommand _saveChangesCommand;
        private PhoneRecordInfo _phoneRecord;
        public PhoneRecordInfo PhoneRecord
        {
            get { return _phoneRecord; }
            set { SetProperty(ref _phoneRecord, value); }
        }

        /// <summary>
        /// Отменяет изменения и закрывает диалоговое окно
        /// </summary>
        public DelegateCommand CancelCommand =>
           _cancelCommand ??= _cancelCommand = new(() =>
           {
               var result = new DialogResult(ButtonResult.Cancel);
               RaiseRequestClose(result);
           });


        
        /// <summary>
        /// Выполняет сохранение изменений
        /// </summary>
        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);

        void ExecuteSaveChangesCommand()
        {            
            DialogResult result = new DialogResult(ButtonResult.OK);
            result.Parameters.Add(DialogNames.NewRecord, PhoneRecord);
            RaiseRequestClose(result);
        }


        public override void OnDialogOpened(IDialogParameters parameters)
        {
            PhoneRecord=parameters.GetValue<PhoneRecordInfo>(DialogNames.NewRecord);            
        }
    }
}
