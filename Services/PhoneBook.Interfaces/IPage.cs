using System;
using System.Collections.Generic;

namespace PhoneBook.Interfaces
{
    /// <summary>
    /// Интерфейс страницы, на которой отображаются записи справочника
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPage<T> where T : IEntity<Guid>
    {
        /// <summary>
        /// Записи справочника
        /// </summary>
        IEnumerable<T> Items { get; }
        /// <summary>
        /// Общее количество записей
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// Размер страницы (сколько элементов на странице)
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Общее количество страниц,с текущим количеством элементов на страницу
        /// </summary>
        int TotalPageCount => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// отвечает на вопрос есть ли предыдущая страница при переключении между страницами 
        /// </summary>
        public bool HasPreviousPage { get; }

        /// <summary>
        /// отвечает на вопрос есть ли следующая страница при переключении между страницами 
        /// </summary>
        public bool HasNextPage { get; }
    }
}
