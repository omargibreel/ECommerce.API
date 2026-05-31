using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOs;

namespace ECommerce.Services.Abstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
