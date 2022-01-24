using PhoneBookLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLib.Repository
{
    public static class RepositoryExtensions
    {
        public static async Task<IEnumerable<T>> GetAllAsync<T>(this IRepository<T> rep,Func<T,bool> filterExpression, CancellationToken cancel=default) 
            where T : class, IEntity, new()
        => (await rep.GetAllAsync(cancel).ConfigureAwait(false)).Where(filterExpression);


        /// <summary>
        /// Возвращает объект page, содержащий элементы удовлетворяющие условию делегата filterExpression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rep">что-то что реализует интерфейс хранилища</param>
        /// <param name="filterExpression">делегат вида Func<T,bool> для фильтрации записей по условию</param>
        /// <param name="cancel">токен отмены асинхронной операции</param>
        /// <returns></returns>
        public static async Task<IPage<T>> GetPage<T>(this IRepository<T> rep,Func<T,bool> filterExpression, CancellationToken cancel = default)
            where T : class, IEntity, new()
        {
            var items=await rep.WhereAsync(filterExpression,cancel).ConfigureAwait(false);            
            return new Page<T>(items, items.Count(), 0, items.Count()); ;
        }
    }
}
