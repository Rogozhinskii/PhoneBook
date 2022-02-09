using PhoneBook.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Сервис аутентификации для WPF
    /// </summary>
    public interface IAuthentificationService
    {
        public string AuthenticatedUserName { get; }
        public string AuthenticatedUserRole { get; }

        /// <summary>
        /// Вход в приложение
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IAuthentificationResult> Login(IUserLogin login,CancellationToken cancel=default);


        Task<IAuthentificationResult> RegisterUser(IUserLogin userModel,CancellationToken cancel=default);

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> Logout(CancellationToken cancel = default);

        /// <summary>
        /// Возвращает роль пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<string> GetUserRoleAsync(string userName, CancellationToken cancel = default);

    }
}
