using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Restaurant.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            order.OrderNumber = await _orderRepository.GenerateOrderNumberAsync();
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Beklemede";
            order.CreatedDate = DateTime.UtcNow;
            
            var createdOrder = await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<bool> UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(updateOrderDto.Id);
            if (existingOrder == null)
                return false;

            _mapper.Map(updateOrderDto, existingOrder);
            existingOrder.UpdatedDate = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(existingOrder);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return false;

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByTableAsync(int tableId)
        {
            var orders = await _orderRepository.GetOrdersByTableAsync(tableId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
            if (order == null) return 0;

            decimal total = order.OrderItems.Sum(item => item.TotalAmount);
            order.TotalAmount = total;
            order.TaxAmount = total * 0.18m; // %18 KDV
            order.FinalAmount = total + order.TaxAmount - order.DiscountAmount;

            return order.FinalAmount;
        }

        public async Task<string> GenerateOrderNumberAsync()
        {
            return await _orderRepository.GenerateOrderNumberAsync();
        }

        public async Task<bool> ProcessOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
            if (order == null) return false;

            // Stok kontrolü ve güncelleme
            foreach (var item in order.OrderItems)
            {
                var stock = await _stockRepository.GetStockByProductAndLocationAsync(item.ProductId, "Depo");
                if (stock != null && stock.Quantity >= item.Quantity)
                {
                    await _stockRepository.UpdateStockQuantityAsync(item.ProductId, "Depo", stock.Quantity - item.Quantity);
                    item.Status = "Hazırlanıyor";
                }
                else
                {
                    return false; // Yetersiz stok
                }
            }

            order.Status = "Hazırlanıyor";
            order.UpdatedDate = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);

            return true;
        }
    }
} 