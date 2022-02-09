using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WPF.Core;
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
        private readonly ITokenHandler _tokenHandler;

        public AuthentificationModel(IAuthentificationService authenticationService,IEventAggregator eventAggregator, ITokenHandler tokenHandler)
        {
            _authenticationService = authenticationService;
            _eventAggregator = eventAggregator;
            _tokenHandler = tokenHandler;
        }
        public async Task<bool> Login(UserLogin userLogin, CancellationToken token = default)
        {
            var result=await _authenticationService.Login(userLogin, token);
            if (result.Success)
            {
                _tokenHandler.Token = result.Token;
                _eventAggregator.GetEvent<LoginEvent>().Publish(userLogin.UserName);
            }
               
            return result.Success;
        }

        public async Task<bool> Logout(CancellationToken token = default)
        {
            _tokenHandler.Token=null;
            return await _authenticationService.Logout(token);
        }
            
        
    }
}
