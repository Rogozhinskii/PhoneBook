using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс доступа к хранилищу через api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWebRepository<T> where T : IEntity,new()
    {
        /// <summary>
        /// Выполнит удаление записи по  id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> DeleteByIdAsync(int id, string token, CancellationToken cancel = default);

        /// <summary>
        /// Добавит новую запись в хранилище
        /// </summary>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> AddAsync(T item, string token, CancellationToken cancel = default);

        /// <summary>
        /// Выполнят обновление записи. защищенное соединение
        /// </summary>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> UpdateAsync(T item, string token, CancellationToken cancel = default);

        /// <summary>
        /// Обновление записи без токена
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> UpdateAsync(T item, CancellationToken cancel = default);

        /// <summary>
        /// Вернет страницу с заданным количеством элементов на ней
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default);
        /// <summary>
        /// вернет страницу с отфильтрованными элементами
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<IPage<T>> GetPage(string filterString, CancellationToken cancel = default);

        /// <summary>
        /// Вернет запись по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> GetByIdAsync(int id, CancellationToken cancel = default);

        /// <summary>
        /// вернет все записи в хранилище
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);

        /// <summary>
        /// Выполнит удаление записи из хранилища
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> DeleteAsync(T item, CancellationToken cancel = default);

        /// <summary>
        /// Добавит новую запись в хранилище
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<T> AddAsync(T item, CancellationToken cancel = default);
    }
}
