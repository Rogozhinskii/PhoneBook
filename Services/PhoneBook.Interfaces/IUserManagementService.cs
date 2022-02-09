using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PhoneBook.Common.Models;
using PhoneBook.Domain;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс через который можно взаимодействовать с api для управления ролями и пользователями
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Возвращает перечисление всех ролей
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationRole>> GetRoles(string token,CancellationToken cancel=default);

        /// <summary>
        /// Вернет роль по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<ApplicationRole> GetRoleById(string id,string token,CancellationToken cancel=default);
        /// <summary>
        /// Выполняет обновление роли
        /// </summary>
        /// <param name="role"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> UpdateRole(ApplicationRole role,string token,CancellationToken cancel=default);
        /// <summary>
        /// Выполняет создание роли 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> CreateRole(ApplicationRole role,string token,CancellationToken cancel=default);
        /// <summary>
        /// Выполняет удаление роли 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> DeleteRole(ApplicationRole role, string token, CancellationToken cancel = default);
        /// <summary>
        /// Вернет всхе пользователей
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllUsers(string token, CancellationToken cancel = default);
        /// <summary>
        /// Создать пользователя, вернут результат создания
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> CreateUser(UserInfo user, string token, CancellationToken cancel = default);

        /// <summary>
        /// Вернет пользователя по его id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<User> GetUserById(string id, string token, CancellationToken cancel = default);
        /// <summary>
        /// вернет роли пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IList<string>> GetUserRoles(string userId, string token, CancellationToken cancel = default);
        /// <summary>
        /// Выполняет обновление информации о пользователе
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(User user, string token, CancellationToken cancel = default);
        /// <summary>
        /// Удалит у пользователя роль
        /// </summary>
        /// <param name="user"></param>
        /// <param name="existingRole"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> RemoveFromRole(User user,string existingRole, string token, CancellationToken cancel = default);
        /// <summary>
        /// Добавит пользователю роль
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newRole"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> AddToRole(User user,string newRole, string token, CancellationToken cancel = default);
        /// <summary>
        /// удалит пользователя по id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> DeleteUserById(string id, string token, CancellationToken cancel = default);
        /// <summary>
        /// Вернет id роли по ее названию
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<string> GetRoleIdByName(string roleName,string token, CancellationToken cancel = default);

        
    }
}
