using PhoneBook.Common.Models;
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

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IAuthentificationResult> RegisterUser(UserInfo userModel,CancellationToken cancel=default);

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> Logout(CancellationToken cancel = default);

       

    }
}
