using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByNameAsync(string name)
        {
            return await _context.Ingredients
                .Where(i => i.Name.Contains(name) && i.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetLowStockIngredientsAsync()
        {
            return await _context.Ingredients
                .Include(i => i.Stocks)
                .Where(i => i.IsActive && i.Stocks.Any(s => s.Quantity <= s.MinimumStock))
                .ToListAsync();
        }
    }
} 