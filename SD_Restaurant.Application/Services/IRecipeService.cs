using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
        Task<RecipeDto> GetRecipeByIdAsync(int id);
        Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto createRecipeDto);
        Task<bool> UpdateRecipeAsync(UpdateRecipeDto updateRecipeDto);
        Task<bool> DeleteRecipeAsync(int id);
        Task<IEnumerable<RecipeDto>> GetRecipesByProductAsync(int productId);
        Task<IEnumerable<RecipeDto>> GetRecipesByIngredientAsync(int ingredientId);
        Task<RecipeDto> GetRecipeByProductAndIngredientAsync(int productId, int ingredientId);
    }
} 