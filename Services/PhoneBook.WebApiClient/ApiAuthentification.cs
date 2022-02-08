using PhoneBook.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WebApiClient
{
    public class ApiAuthentification : IAuthentificationService
    {
        public string AuthenticatedUserName => throw new System.NotImplementedException();

        public string AuthenticatedUserRole => throw new System.NotImplementedException();

        public Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Login(IUserLogin login, CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Logout(CancellationToken cancel = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
