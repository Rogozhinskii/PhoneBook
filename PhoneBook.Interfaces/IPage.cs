using System;
using System.Collections.Generic;

namespace PhoneBook.Interfaces
{
    public interface IPage<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}
