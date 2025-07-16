using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsWithRecipesAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Recipes)
                .ThenInclude(r => r.Ingredient)
                .Where(p => p.IsRecipe && p.IsActive)
                .ToListAsync();
        }

        public async Task<Product?> GetProductWithRecipesAsync(int productId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Recipes)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            return await _dbSet
                .Include(p => p.Stocks)
                .Where(p => p.IsActive && p.Stocks.Any(s => s.Quantity <= s.MinimumStock))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IsActive && (p.Name.Contains(searchTerm) || (p.Description != null && p.Description.Contains(searchTerm))))
                .ToListAsync();
        }
    }
} 