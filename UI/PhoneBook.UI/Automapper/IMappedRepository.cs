using PhoneBook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Automapper
{
    /// <summary>
    /// Интерфейс для объектно-объектного преобразования сущностей хранилища и вьюмоделей представления
    /// </summary>
    /// <typeparam name="T">тип вью модели</typeparam>
    /// <typeparam name="TBase">тип сущности</typeparam>
    public interface IMappedRepository<T, TBase>
        where T : IEntity, new()
        where TBase : IEntity, new()
    {
        /// <summary>
        /// Возаращает viemodel сущности по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Возвращает страницу наполенную вью моделями сущностей, которые удовлетворяют условию делегата Func<TBase,bool>
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(Func<TBase, bool> filterExpression, CancellationToken cancel = default);

        /// <summary>
        /// Возвращает страницу наполенную количеством вью моделей сущностей равным pageSize
        /// </summary>        
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество элементов на странице</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default);
    }
}