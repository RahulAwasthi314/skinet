using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
            
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(int id) 
        { 
            return await _productRepository.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands() {
            var productBrands = await _productRepository.GetProductBrandsAsync();
            return Ok(productBrands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand?>> GetProductBrand(int id) 
        { 
            return await _productRepository.GetProductBrandByIdAsync(id);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes() {
            var products = await _productRepository.GetProductTypesAsync();
            return Ok(products);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType?>> GetProductType(int id) 
        { 
            return await _productRepository.GetProductTypeByIdAsync(id);
        }
    }
}