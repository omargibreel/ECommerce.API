using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Implementation.Specifications.ProductSpecifications
{
    internal class ProductWithBrandAndCategorySpecification : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecification(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.ProductBrandId == queryParams.BrandId.Value)
            && (!queryParams.CategoryId.HasValue || p.ProductCategoryId == queryParams.CategoryId.Value)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductCategory);
        }
        public ProductWithBrandAndCategorySpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductCategory);
        }
    }
}
