using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        #region Relationships

        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        #endregion
    }
}
