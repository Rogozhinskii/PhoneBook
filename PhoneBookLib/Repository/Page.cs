using PhoneBookLib.Interfaces;
using System;
using System.Collections.Generic;

namespace PhoneBookLib
{
    public class Page<T> : IPage<T> 
    {
        public Page(IEnumerable<T> items,int totalCount,int pageIndex,int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;

        }

        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex{ get; set; }

        public int PageSize { get; set; }
        int TotalPageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
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
    //public class Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T> where T : class, IEntity, new()
    //{ }
    //public partial class DbRepository<T> 
    //{

    //}
}
