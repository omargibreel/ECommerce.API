using ECommerce.Domain.Models.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Implementation.Specifications.ProductSpecifications
{
    internal class ProductWithCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecification(ProductQueryParams queryParams) : base(ProductSpecificationsHelper.GetCriteria(queryParams))
        {

        }
    }
}
