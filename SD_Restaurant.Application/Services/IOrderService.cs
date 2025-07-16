using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<bool> UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderDto>> GetOrdersByTableAsync(int tableId);
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
        Task<string> GenerateOrderNumberAsync();
        Task<bool> ProcessOrderAsync(int orderId);
    }
} 