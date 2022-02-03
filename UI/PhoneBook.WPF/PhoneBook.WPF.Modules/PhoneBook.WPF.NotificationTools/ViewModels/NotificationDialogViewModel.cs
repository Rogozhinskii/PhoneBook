using PhoneBook.WPF.Core;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace PhoneBook.WPF.NotificationTools.ViewModels
{
    public class NotificationDialogViewModel:DialogViewModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DelegateCommand _closeDialogCommand;

        public DelegateCommand CloseDialogCommand =>
           _closeDialogCommand ??= _closeDialogCommand = new(() =>
           {
               DialogResult result = new(ButtonResult.OK);
               RaiseRequestClose(result);
           });

      
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(DialogNames.DialogMessage);
        }
    }
}
