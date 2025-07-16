using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByProductAsync(int productId)
        {
            return await _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.Ingredient)
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByIngredientAsync(int ingredientId)
        {
            return await _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.Ingredient)
                .Where(r => r.IngredientId == ingredientId)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByProductAndIngredientAsync(int productId, int ingredientId)
        {
            return await _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.Ingredient)
                .FirstOrDefaultAsync(r => r.ProductId == productId && r.IngredientId == ingredientId);
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesWithDetailsAsync()
        {
            return await _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.Ingredient)
                .ToListAsync();
        }
    }
} 