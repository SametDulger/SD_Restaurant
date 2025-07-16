using SD_Restaurant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<Category?> GetCategoryWithProductsAsync(int categoryId);
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
    }
} 