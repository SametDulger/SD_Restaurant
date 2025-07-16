using SD_Restaurant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipesByProductAsync(int productId);
        Task<IEnumerable<Recipe>> GetRecipesByIngredientAsync(int ingredientId);
        Task<Recipe?> GetRecipeByProductAndIngredientAsync(int productId, int ingredientId);
        Task<IEnumerable<Recipe>> GetAllRecipesWithDetailsAsync();
    }
} 