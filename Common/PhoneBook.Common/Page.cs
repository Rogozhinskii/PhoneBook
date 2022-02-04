using PhoneBook.Interfaces;
using System;
using System.Collections.Generic;

namespace PhoneBook.Common
{
    /// <summary>
    /// Модель страницы с определенным количеством записей
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : IPage<T> where T : IEntity
    {        
        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex{ get; set; }

        public int PageSize { get; set; }
        public int TotalPageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPageCount-1);
            }
        }
    }
   
}
