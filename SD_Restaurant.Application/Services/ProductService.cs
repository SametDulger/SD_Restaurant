using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedDate = DateTime.UtcNow;
            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updateProductDto.Id);
            if (existingProduct == null)
                return false;

            _mapper.Map(updateProductDto, existingProduct);
            existingProduct.UpdatedDate = DateTime.UtcNow;
            await _productRepository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var products = await _productRepository.SearchProductsAsync(searchTerm);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
        {
            var products = await _productRepository.GetLowStockProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<decimal> CalculateProductCostAsync(int productId)
        {
            var product = await _productRepository.GetProductWithRecipesAsync(productId);
            if (product == null || !product.IsRecipe)
                return 0;

            decimal totalCost = 0;
            foreach (var recipe in product.Recipes)
            {
                var stock = await _stockRepository.GetStockByProductAndLocationAsync(recipe.IngredientId, "Depo");
                if (stock != null)
                {
                    totalCost += stock.Cost * recipe.Quantity;
                }
            }

            return totalCost;
        }
    }
} 