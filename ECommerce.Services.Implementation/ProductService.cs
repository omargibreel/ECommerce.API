using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Shared.DTOs.ProductDTOs;
using ECommerce.Services.Implementation.Specifications.ProductSpecifications;
using ECommerce.Shared;
using ECommerce.Services.Implementation.Exceptions;
using ECommerce.Shared.CommonResponses;

namespace ECommerce.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductDTO>?> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandAndCategorySpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);

            if (product == null)
                return Error.NotFound("Product.NotFound", $"Product with id {id} not found.");
            
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndCategorySpecification(queryParams);
            var products = await repo.GetAllAsync(specification);
            var data = _mapper.Map<IEnumerable<ProductDTO>>(products);

            var productWithCountSpecification = new ProductWithCountSpecification(queryParams);
            var totalCount = await repo.CountAsync(productWithCountSpecification);
            return new PaginatedResult<ProductDTO>
            {
                PageIndex = queryParams.PageIndex,
                PageSize = data.Count(),
                Count = totalCount,
                Data = data
            };
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }
    }
}
