using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper
            )
        {
            this._productRepository = productRepository;
            this._productBrandRepository = productBrandRepository;
            this._productTypeRepository = productTypeRepository;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts() {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepository.ListAsync(spec);

            return Ok(_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetProduct(int id) 
        { 
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            
            var product = await _productRepository.GetEntityWithSpec(spec);

            if (product == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Product, ProductDto>(product));
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