using PhoneBook.WPF.Core;
using PhoneBook.WPF.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;

namespace PhoneBook.WPF.NotificationTools.ViewModels
{
    public class HeaderControlViewModel:BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _showLoginDialog;

        public HeaderControlViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoginEvent>().Subscribe((userName) =>
            {
                Welcome =$"Hello {userName}!";
                WelcomeTextBoxVisibility=Visibility.Visible;
            });
        }


        private string _welcome;
        public string Welcome
        {
            get { return _welcome; }
            set { SetProperty(ref _welcome, value); }
        }

        private Visibility _welcomeTextBoxVisibility=Visibility.Hidden;
        public Visibility WelcomeTextBoxVisibility
        {
            get { return _welcomeTextBoxVisibility; }
            set { SetProperty(ref _welcomeTextBoxVisibility, value); }
        }




        public DelegateCommand ShowLoginDialog =>
           _showLoginDialog ??= _showLoginDialog = new(() =>
           {
               _dialogService.ShowDialog(DialogNames.AuthentificationDialog, null, r => { });
           });
        


    }
}
