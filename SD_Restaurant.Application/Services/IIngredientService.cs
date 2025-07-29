using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task<IngredientDto> GetIngredientByIdAsync(int id);
        Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto createIngredientDto);
        Task<bool> UpdateIngredientAsync(UpdateIngredientDto updateIngredientDto);
        Task<bool> DeleteIngredientAsync(int id);
        Task<IEnumerable<IngredientDto>> GetIngredientsByNameAsync(string name);
        Task<IEnumerable<IngredientDto>> GetLowStockIngredientsAsync();
    }
} 