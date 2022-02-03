using PhoneBook.WPF.Core;
using PhoneBook.WPF.Core.Events;
using PhoneBook.WPF.Core.Interfaces;
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
        private readonly IAuthentificationModel _authenticationModel;
        private DelegateCommand _showLoginDialog;

        public HeaderControlViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IAuthentificationModel authenticationModel)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _authenticationModel = authenticationModel;
            _eventAggregator.GetEvent<LoginEvent>().Subscribe((userName) =>
            {
                Welcome =$"Hello {userName}!";
                WelcomeTextBoxVisibility=Visibility.Visible;
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
            set { SetProperty(ref _filterText, value); _eventAggregator.GetEvent<FilterTextChanged>().Publish(_filterText); }
        }

        #endregion



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

        private DelegateCommand _logoutCommand;

        public DelegateCommand LogoutCommand =>
           _logoutCommand ??= _logoutCommand = new(async() =>
           {               
               if(await _authenticationModel.Logout())
               {
                   Welcome = string.Empty;
                   WelcomeTextBoxVisibility = Visibility.Hidden;
               }
           });
        



    }
}
