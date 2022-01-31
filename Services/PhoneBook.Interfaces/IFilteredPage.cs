using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    public interface IFilteredPage<T> where T : IEntity
    {
        /// <summary>
        /// Возвращает страницу наполненную перечислением сущностей, удовлетворяющих значению искомой строки
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(string filterString, CancellationToken cancel = default);
    }
}
