using ECommerce.Presentation.Attributes;
using ECommerce.Services.Abstraction;
using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers
{
    
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        // GET: baseUrl/api/products
        [RedisCache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams) 
        {
            var products = await _productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        // GET: baseUrl/api/products/{id}
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return HandleResult<ProductDTO>(result);
        }

        [HttpGet("brands")]
        // GET: baseUrl/api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        // GET: baseUrl/api/products/types
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}