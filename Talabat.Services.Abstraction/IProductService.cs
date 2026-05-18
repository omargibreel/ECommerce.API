using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Shared.DTOs.ProductDTOs;

namespace Talabat.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
