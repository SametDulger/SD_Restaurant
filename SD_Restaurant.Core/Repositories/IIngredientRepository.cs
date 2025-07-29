using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsByNameAsync(string name);
        Task<IEnumerable<Ingredient>> GetLowStockIngredientsAsync();
    }
} 