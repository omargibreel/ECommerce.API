using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Services.Abstraction;
using Talabat.Shared.DTOs.ProductDTOs;

namespace Talabat.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        // GET: baseUrl/api/products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        // GET: baseUrl/api/products/{id}
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
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