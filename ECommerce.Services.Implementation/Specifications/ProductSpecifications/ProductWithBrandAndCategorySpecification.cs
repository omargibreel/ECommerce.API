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
            : base(ProductSpecificationsHelper.GetCriteria(queryParams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductCategory);


            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        public ProductWithBrandAndCategorySpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductCategory);
        }
    }
}
