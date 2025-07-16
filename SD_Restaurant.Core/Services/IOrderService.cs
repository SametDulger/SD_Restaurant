using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
        Task<string> GenerateOrderNumberAsync();
        Task<bool> ProcessOrderAsync(int orderId);
    }
} 