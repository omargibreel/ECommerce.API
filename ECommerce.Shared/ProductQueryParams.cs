using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }
    }
}
