using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Repositories
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock?> GetStockByProductAndLocationAsync(int productId, string location);
        Task<IEnumerable<Stock>> GetStocksByLocationAsync(string location);
        Task<IEnumerable<Stock>> GetLowStockItemsAsync();
        Task UpdateStockQuantityAsync(int productId, string location, decimal quantity);
        Task<IEnumerable<Stock>> GetStocksByProductAsync(int productId);
    }
} 