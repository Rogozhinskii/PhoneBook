using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Interfaces
{
    public interface IAuthentication
    {
        Task<bool> Login(IUserLogin login,CancellationToken cancel=default);
    }
}
