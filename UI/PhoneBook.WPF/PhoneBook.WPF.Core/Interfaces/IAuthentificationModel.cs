using PhoneBook.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.WPF.Core.Interfaces
{
    /// <summary>
    /// Модель для взаимодейтсвия с клиентом авторизации в api
    /// </summary>
    public interface IAuthentificationModel
    {
        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> Login(UserLogin userLogin,CancellationToken token=default);

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> Logout(CancellationToken token=default);
    }
}
