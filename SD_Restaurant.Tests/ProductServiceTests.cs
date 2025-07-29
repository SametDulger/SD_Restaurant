using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using Xunit;

namespace SD_Restaurant.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IStockRepository> _mockStockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockStockRepository = new Mock<IStockRepository>();
            _mockMapper = new Mock<IMapper>();
            _productService = new ProductService(_mockProductRepository.Object, _mockStockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnMappedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1", Price = 10.0m },
                new Product { Id = 2, Name = "Test Product 2", Price = 20.0m }
            };

            var expectedDtos = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Test Product 1", Price = 10.0m },
                new ProductDto { Id = 2, Name = "Test Product 2", Price = 20.0m }
            };

            _mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(x => x.Map<IEnumerable<ProductDto>>(products)).Returns(expectedDtos);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDtos, result);
            _mockProductRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<ProductDto>>(products), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId, Name = "Test Product", Price = 10.0m };
            var expectedDto = new ProductDto { Id = productId, Name = "Test Product", Price = 10.0m };

            _mockProductRepository.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(x => x.Map<ProductDto>(product)).Returns(expectedDto);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto, result);
            _mockProductRepository.Verify(x => x.GetByIdAsync(productId), Times.Once);
            _mockMapper.Verify(x => x.Map<ProductDto>(product), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var productId = 999;
            _mockProductRepository.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync((Product?)null);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
            _mockProductRepository.Verify(x => x.GetByIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task CreateProductAsync_WithValidDto_ShouldReturnCreatedProduct()
        {
            // Arrange
            var createDto = new CreateProductDto { Name = "New Product", Price = 15.0m };
            var product = new Product { Id = 1, Name = "New Product", Price = 15.0m };
            var expectedDto = new ProductDto { Id = 1, Name = "New Product", Price = 15.0m };

            _mockMapper.Setup(x => x.Map<Product>(createDto)).Returns(product);
            _mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);
            _mockMapper.Setup(x => x.Map<ProductDto>(product)).Returns(expectedDto);

            // Act
            var result = await _productService.CreateProductAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto, result);
            Assert.Equal(DateTime.UtcNow.Date, product.CreatedDate.Date);
            _mockMapper.Verify(x => x.Map<Product>(createDto), Times.Once);
            _mockProductRepository.Verify(x => x.AddAsync(product), Times.Once);
            _mockMapper.Verify(x => x.Map<ProductDto>(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidDto_ShouldReturnTrue()
        {
            // Arrange
            var updateDto = new UpdateProductDto { Id = 1, Name = "Updated Product", Price = 25.0m };
            var existingProduct = new Product { Id = 1, Name = "Old Product", Price = 15.0m };

            _mockProductRepository.Setup(x => x.GetByIdAsync(updateDto.Id)).ReturnsAsync(existingProduct);
            _mockProductRepository.Setup(x => x.UpdateAsync(existingProduct)).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.UpdateProductAsync(updateDto);

            // Assert
            Assert.True(result);
            Assert.Equal(DateTime.UtcNow.Date, existingProduct.UpdatedDate?.Date);
            _mockProductRepository.Verify(x => x.GetByIdAsync(updateDto.Id), Times.Once);
            _mockMapper.Verify(x => x.Map(updateDto, existingProduct), Times.Once);
            _mockProductRepository.Verify(x => x.UpdateAsync(existingProduct), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var updateDto = new UpdateProductDto { Id = 999, Name = "Updated Product", Price = 25.0m };
            _mockProductRepository.Setup(x => x.GetByIdAsync(updateDto.Id)).ReturnsAsync((Product?)null);

            // Act
            var result = await _productService.UpdateProductAsync(updateDto);

            // Assert
            Assert.False(result);
            _mockProductRepository.Verify(x => x.GetByIdAsync(updateDto.Id), Times.Once);
            _mockProductRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
} 