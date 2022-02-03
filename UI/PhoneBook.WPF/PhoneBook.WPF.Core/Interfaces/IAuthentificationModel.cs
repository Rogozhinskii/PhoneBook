using PhoneBook.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.Core.Interfaces
{
    public interface IAuthentificationModel
    {
        Task<bool> Login(UserLogin userLogin,CancellationToken token=default);
        Task<bool> Logout(CancellationToken token=default);
    }
}
