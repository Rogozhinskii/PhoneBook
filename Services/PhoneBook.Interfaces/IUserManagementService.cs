using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Interfaces
{
    public interface IUserManagementService
    {
        Task<IEnumerable<IdentityRole>> GetRoles(string token,CancellationToken cancel=default);
        Task<IdentityRole> GetRoleById(string id,string token,CancellationToken cancel=default);
        Task<bool> UpdateRole(IdentityRole role,string token,CancellationToken cancel=default);
        Task<bool> CreateRole(IdentityRole role,string token,CancellationToken cancel=default);
        Task<bool> DeleteRole(IdentityRole role, string token, CancellationToken cancel = default);
        Task<IEnumerable<IdentityUser>> GetAllUsers(string token, CancellationToken cancel = default);
        Task<bool> CreateUser(IUserLogin user, string token, CancellationToken cancel = default);
        Task<IdentityUser> GetUserById(string id, string token, CancellationToken cancel = default);
        Task<IList<string>> GetUserRoles(string userId, string token, CancellationToken cancel = default);
        Task<bool> UpdateUser(IdentityUser user, string token, CancellationToken cancel = default);
        Task<bool> RemoveFromRole(IdentityUser user,string existingRole, string token, CancellationToken cancel = default);
        Task<bool> AddToRole(IdentityUser user,string newRole, string token, CancellationToken cancel = default);

        Task<string> GetRoleIdByName(string roleName,string token, CancellationToken cancel = default);

        Task<bool> TT(User user, string newRole, string token, CancellationToken cancel = default);
    }
}
