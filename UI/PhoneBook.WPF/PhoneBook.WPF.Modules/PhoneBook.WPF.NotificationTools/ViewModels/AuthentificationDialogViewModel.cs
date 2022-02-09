using PhoneBook.Common.Models;
using PhoneBook.WPF.Core;
using PhoneBook.WPF.Core.Extensions;
using PhoneBook.WPF.Core.Interfaces;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PhoneBook.WPF.NotificationTools.ViewModels
{
    public class AuthentificationDialogViewModel:DialogViewModel
    {
        private readonly IAuthentificationModel _authenticationModel;
        
        private DelegateCommand _loginCommand;
        private DelegateCommand<object> _passwordChangedCommand;

        public AuthentificationDialogViewModel(IAuthentificationModel authenticationModel)
        {
            _authenticationModel = authenticationModel;
            
        }


        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private SecureString _password;
        public SecureString Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        /// <summary>
        /// Выполняет аутентификацию
        /// </summary>
        public DelegateCommand LoginCommand =>
           _loginCommand ??= _loginCommand = new(async() =>
           {
               var userLogin = new UserLogin
               {
                   UserName = UserName,
                   Password = Password.GetPasswordAsString()
               };
               var loginResult=await _authenticationModel.Login(userLogin);
               if (loginResult)
               {
                   
                   RaiseRequestClose(ButtonResult.OK);
               }
                   
               else
               {

                   await RejectInputData();
               }
           });

        private async Task RejectInputData()
        {
            Message = "Не верный логин или пароль!";
            UserName = string.Empty;
            Password = null;
            await Task.Delay(2000);
            Message =string.Empty;
        }

        /// <summary>
        /// Вызывается при изменении поля с паролем
        /// </summary>
        public DelegateCommand<object> PasswordChangedCommand =>
           _passwordChangedCommand ??= _passwordChangedCommand = new DelegateCommand<object>((obj) =>
           {
               Password = ((PasswordBox)obj).SecurePassword;
               Password.MakeReadOnly();
           });
        

    }
}
