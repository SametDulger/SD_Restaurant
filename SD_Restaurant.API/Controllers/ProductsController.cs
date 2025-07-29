using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
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
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResult(products, "Ürünler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(ApiResponse<ProductDto>.ErrorResult("Ürün bulunamadı"));
            }
            return Ok(ApiResponse<ProductDto>.SuccessResult(product, "Ürün başarıyla getirildi"));
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResult(products, "Kategori ürünleri başarıyla getirildi"));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> SearchProducts([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(ApiResponse<IEnumerable<ProductDto>>.ErrorResult("Arama terimi gerekli"));
            }

            var products = await _productService.SearchProductsAsync(term);
            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResult(products, "Arama sonuçları"));
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetLowStockProducts()
        {
            var products = await _productService.GetLowStockProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResult(products, "Düşük stok ürünleri"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdProduct = await _productService.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, 
                ApiResponse<ProductDto>.SuccessResult(createdProduct, "Ürün başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _productService.UpdateProductAsync(updateProductDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Ürün bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Ürün başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Ürün bulunamadı"));
            }

            await _productService.DeleteProductAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(new object(), "Ürün başarıyla silindi"));
        }

        [HttpGet("{id}/cost")]
        public async Task<ActionResult<ApiResponse<decimal>>> GetProductCost(int id)
        {
            var cost = await _productService.CalculateProductCostAsync(id);
            return Ok(ApiResponse<decimal>.SuccessResult(cost, "Ürün maliyeti hesaplandı"));
        }

        private List<string> GetModelStateErrors()
        {
            var errors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
} 