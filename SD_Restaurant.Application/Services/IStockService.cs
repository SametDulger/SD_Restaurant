using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockDto>> GetAllStocksAsync();
        Task<StockDto> GetStockByIdAsync(int id);
        Task<StockDto> CreateStockAsync(CreateStockDto createStockDto);
        Task<bool> UpdateStockAsync(UpdateStockDto updateStockDto);
        Task<bool> DeleteStockAsync(int id);
        Task<StockDto> GetStockByProductAndLocationAsync(int productId, string location);
        Task<IEnumerable<StockDto>> GetStocksByLocationAsync(string location);
        Task<IEnumerable<StockDto>> GetLowStockItemsAsync();
        Task UpdateStockQuantityAsync(int productId, string location, decimal quantity);
        Task<bool> CheckStockAvailabilityAsync(int productId, string location, decimal requiredQuantity);
        Task<IEnumerable<StockDto>> GetStocksByProductAsync(int productId);
    }
} 