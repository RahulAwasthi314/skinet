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
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository)
        {
            this._productRepository = productRepository;
            this._productBrandRepository = productBrandRepository;
            this._productTypeRepository = productTypeRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct(int id) 
        { 
            return await _productRepository.GetByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands() {
            var productBrands = await _productBrandRepository.GetAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand?>> GetProductBrand(int id) 
        { 
            return await _productBrandRepository.GetByIdAsync(id);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes() {
            var products = await _productTypeRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType?>> GetProductType(int id) 
        { 
            return await _productTypeRepository.GetByIdAsync(id);
        }
    }
}