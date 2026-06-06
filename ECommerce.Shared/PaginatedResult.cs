using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared
{
    public class PaginatedResult<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public int TotalPages =>
        PageSize <= 0
        ? 0
        : (Count + PageSize - 1) / PageSize;

        public bool HasNextPage =>
            PageIndex < TotalPages;

        public bool HasPreviousPage =>
            PageIndex > 1;

        public IEnumerable<T> Data { get; set; }
    }
}
