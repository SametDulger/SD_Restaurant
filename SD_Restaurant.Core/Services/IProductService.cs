using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Product>> GetLowStockProductsAsync();
        Task<decimal> CalculateProductCostAsync(int productId);
    }
} 