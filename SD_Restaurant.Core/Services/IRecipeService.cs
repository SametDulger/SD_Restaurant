using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
        Task<IEnumerable<Recipe>> GetRecipesByProductAsync(int productId);
        Task<IEnumerable<Recipe>> GetRecipesByIngredientAsync(string ingredientName);
        Task<Recipe> GetRecipeByProductAndIngredientAsync(int productId, string ingredientName);
        Task<decimal> CalculateRecipeCostAsync(int recipeId);
    }
} 