using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PhoneBook.Common.Models;
using PhoneBook.Domain;

namespace PhoneBook.Interfaces
{
    public interface IUserManagementService
    {
        Task<IEnumerable<ApplicationRole>> GetRoles(string token,CancellationToken cancel=default);
        Task<ApplicationRole> GetRoleById(string id,string token,CancellationToken cancel=default);
        Task<bool> UpdateRole(ApplicationRole role,string token,CancellationToken cancel=default);
        Task<bool> CreateRole(ApplicationRole role,string token,CancellationToken cancel=default);
        Task<bool> DeleteRole(ApplicationRole role, string token, CancellationToken cancel = default);
        Task<IEnumerable<User>> GetAllUsers(string token, CancellationToken cancel = default);
        Task<bool> CreateUser(UserInfo user, string token, CancellationToken cancel = default);
        Task<User> GetUserById(string id, string token, CancellationToken cancel = default);
        Task<IList<string>> GetUserRoles(string userId, string token, CancellationToken cancel = default);
        Task<bool> UpdateUser(User user, string token, CancellationToken cancel = default);
        Task<bool> RemoveFromRole(User user,string existingRole, string token, CancellationToken cancel = default);
        Task<bool> AddToRole(User user,string newRole, string token, CancellationToken cancel = default);
        Task<bool> DeleteUserById(string id, string token, CancellationToken cancel = default);
        Task<string> GetRoleIdByName(string roleName,string token, CancellationToken cancel = default);

        
    }
}
