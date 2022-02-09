using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> CanEdit(string token,CancellationToken cancelationToken=default);
        Task<bool> CanDelete(string token,CancellationToken cancelationToken=default);
        Task<bool> CanCreate(string token,CancellationToken cancelationToken=default);
    }
}
