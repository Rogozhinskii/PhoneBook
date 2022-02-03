using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Interfaces
{
    public interface IAuthentificationService
    {
        public string AuthenticatedUserName { get; }
        public string AuthenticatedUserRole { get; }

        Task<bool> Login(IUserLogin login,CancellationToken cancel=default);
        Task<bool> Logout(CancellationToken cancel = default);

        Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default);

    }
}
