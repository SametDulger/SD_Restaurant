using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IStockService
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<Stock> CreateStockAsync(Stock stock);
        Task UpdateStockAsync(Stock stock);
        Task DeleteStockAsync(int id);
        Task<Stock> GetStockByProductAndLocationAsync(int productId, string location);
        Task<IEnumerable<Stock>> GetStocksByLocationAsync(string location);
        Task<IEnumerable<Stock>> GetLowStockItemsAsync();
        Task UpdateStockQuantityAsync(int productId, string location, decimal quantity);
        Task<bool> CheckStockAvailabilityAsync(int productId, string location, decimal requiredQuantity);
        Task<IEnumerable<Stock>> GetStocksByProductAsync(int productId);
    }
} 