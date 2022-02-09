using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс для проверки прав пользователя
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// true, если пользователь может редактировать записи иначе false
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<bool> CanEdit(string token,CancellationToken cancelationToken=default);

        /// <summary>
        /// вернет true, если пользователь может удалить записи иначе false
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<bool> CanDelete(string token,CancellationToken cancelationToken=default);
        /// <summary>
        /// вернет true, если пользователь может создать записи иначе false
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<bool> CanCreate(string token,CancellationToken cancelationToken=default);
    }
}
