using ECommerce.Domain.Models.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Services.Implementation.Specifications.ProductSpecifications
{
    internal static class ProductSpecificationsHelper
    {
        public static Expression<Func<Product,bool>> GetCriteria(ProductQueryParams queryParams)
        {
            return p => (!queryParams.BrandId.HasValue || p.ProductBrandId == queryParams.BrandId.Value)
            && (!queryParams.CategoryId.HasValue || p.ProductCategoryId == queryParams.CategoryId.Value)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }
    }
}
