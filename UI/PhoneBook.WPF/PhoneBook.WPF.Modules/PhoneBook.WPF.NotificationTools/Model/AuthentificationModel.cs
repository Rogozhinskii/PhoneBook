using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core.Events;
using PhoneBook.WPF.Core.Interfaces;
using Prism.Events;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.NotificationTools.Model
{
    public class AuthentificationModel : IAuthentificationModel
    {
        private readonly IAuthentificationService _authenticationService;
        private readonly IEventAggregator _eventAggregator;

        public AuthentificationModel(IAuthentificationService authenticationService,IEventAggregator eventAggregator)
        {
            _authenticationService = authenticationService;
            _eventAggregator = eventAggregator;
        }
        public async Task<bool> Login(UserLogin userLogin, CancellationToken token = default)
        {
            var result=await _authenticationService.Login(userLogin, token);
            if (result)
                _eventAggregator.GetEvent<LoginEvent>().Publish(userLogin.UserName);
            return result;
        }

        public async Task<bool> Logout(CancellationToken token = default) =>
            await _authenticationService.Logout(token);
        
    }
}
