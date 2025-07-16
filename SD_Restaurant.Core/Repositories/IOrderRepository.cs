using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<Order?> GetOrderWithItemsAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId);
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
        Task<string> GenerateOrderNumberAsync();
    }
} 