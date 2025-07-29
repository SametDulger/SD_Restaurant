using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        public StockRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<Stock?> GetStockByProductAndLocationAsync(int productId, string location)
        {
            return await _dbSet
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.Location == location && s.IsActive);
        }

        public async Task<Stock?> GetStockByIngredientAndLocationAsync(int ingredientId, string location)
        {
            return await _dbSet
                .Include(s => s.Ingredient)
                .FirstOrDefaultAsync(s => s.IngredientId == ingredientId && s.Location == location && s.IsActive);
        }

        public async Task<IEnumerable<Stock>> GetStocksByLocationAsync(string location)
        {
            return await _dbSet
                .Include(s => s.Product)
                .Include(s => s.Ingredient)
                .Where(s => s.Location == location && s.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetLowStockItemsAsync()
        {
            return await _dbSet
                .Include(s => s.Product)
                .Include(s => s.Ingredient)
                .Where(s => s.Quantity <= s.MinimumStock && s.IsActive)
                .ToListAsync();
        }

        public async Task UpdateStockQuantityAsync(int productId, string location, decimal quantity)
        {
            var stock = await GetStockByProductAndLocationAsync(productId, location);
            if (stock != null)
            {
                stock.Quantity = quantity;
                stock.UpdatedDate = System.DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Stock>> GetStocksByProductAsync(int productId)
        {
            return await _dbSet
                .Include(s => s.Product)
                .Where(s => s.ProductId == productId && s.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksByIngredientAsync(int ingredientId)
        {
            return await _dbSet
                .Include(s => s.Ingredient)
                .Where(s => s.IngredientId == ingredientId && s.IsActive)
                .ToListAsync();
        }
    }
} 