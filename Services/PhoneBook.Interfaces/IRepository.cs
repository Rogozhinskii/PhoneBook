using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа к хранилищу данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IEntity, new()
    {
        /// <summary>
        /// Возвращает сущность по ее идентификационному номеру
        /// </summary>
        /// <param name="id">первичный ключ</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id,CancellationToken cancel=default);

        /// <summary>
        /// Добавляет сущность в хранилище, возвращает сущность с присвоенным идентификационным номером
        /// </summary>
        /// <param name="item">добавляемая сущность</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> AddAsync(T item,CancellationToken cancel=default);

        /// <summary>
        /// Выполняет обновление сущности в хранилище, возвращает обновленную сущность
        /// </summary>
        /// <param name="item">обновляемая сущность</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T item,CancellationToken cancel=default);

        /// <summary>
        /// Выполняет удаление сущности из хранилища
        /// </summary>
        /// <param name="item">удаляемая сущность</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(T item,CancellationToken cancel=default);

        /// <summary>
        /// Выполняет удаление сущности их хранилища по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default);

        /// <summary>
        /// Возвращает количество записей в хранилище
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(CancellationToken cancel = default);

        /// <summary>
        /// Возвращает вме имеющиеся записи типа Т из хранилища
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);

        /// <summary>
        /// Выполняет сохранение всех изменений отслеживаемых сущностей
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancel = default);

        /// <summary>
        /// Возвращает страницу с заданным количеством элементов на ней
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">размер страницы</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(int pageIndex,int pageSize,CancellationToken cancel=default);

        /// <summary>
        /// Возвращает перечисление сущностей удовлетворяющих заданному условия делегата Func<T,bool>
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> WhereAsync(Func<T, bool> filterExpression, CancellationToken cancel = default);

        /// <summary>
        /// Изменяет режим сохранения изменений в хранилище. если autoSaveChanges is true, то сохранения будут выполняться автоматически
        /// </summary>
        /// <param name="autoSaveChanges">флаг включения автосохранения</param>
        void ChangeSaveMode(bool autoSaveChanges);

        /// <summary>
        /// Определяет есть ли запись в хранилище с указанным id, true-вернет если запись найдена, 
        /// иначе false
        /// </summary>
        /// <param name="id">идентификатор искомой записи</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(int id, CancellationToken cancel = default);

        /// <summary>
        /// Определяет есть ли указанная сущность в хранилище. true-вернет если запись найдена, иначе false
        /// </summary>
        /// <param name="item">искомая сущность</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> ExistAsync(T item, CancellationToken cancel = default);

        /// <summary>
        /// Возвращает перечисление сущностей типа Т
        /// </summary>
        /// <param name="skip">количество записей, которых нужно пропустить</param>
        /// <param name="count">количество записей, которых нужно извлечь из хранилища</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default);


        /// <summary>
        /// Возвращает страницу наполненную перечислением сущностей, удовлетворяющих значению искомой строки
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(string filterString, CancellationToken cancel = default);
    }
}
