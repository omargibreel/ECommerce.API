using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Domain.Models.ProductModule
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
