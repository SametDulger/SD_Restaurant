using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResult(orders, "Siparişler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(ApiResponse<OrderDto>.ErrorResult("Sipariş bulunamadı"));
            }
            return Ok(ApiResponse<OrderDto>.SuccessResult(order, "Sipariş başarıyla getirildi"));
        }

        [HttpGet("table/{tableId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetOrdersByTable(int tableId)
        {
            var orders = await _orderService.GetOrdersByTableAsync(tableId);
            return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResult(orders, "Masa siparişleri başarıyla getirildi"));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetOrdersByStatus(string status)
        {
            if (Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
            {
                var orders = await _orderService.GetOrdersByStatusAsync(orderStatus);
                return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResult(orders, "Durum bazlı siparişler getirildi"));
            }
            return BadRequest(ApiResponse<IEnumerable<OrderDto>>.ErrorResult("Geçersiz durum değeri"));
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrderDto>>> CreateOrder(CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<OrderDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdOrder = await _orderService.CreateOrderAsync(createOrderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, 
                ApiResponse<OrderDto>.SuccessResult(createdOrder, "Sipariş başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            if (id != updateOrderDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _orderService.UpdateOrderAsync(updateOrderDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Sipariş bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Sipariş başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Sipariş bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Sipariş başarıyla silindi"));
        }

        [HttpPost("{id}/process")]
        public async Task<ActionResult<bool>> ProcessOrder(int id)
        {
            var result = await _orderService.ProcessOrderAsync(id);
            if (!result)
            {
                return BadRequest("Order could not be processed. Check stock availability.");
            }
            return Ok(result);
        }

        [HttpGet("{id}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotal(int id)
        {
            var total = await _orderService.CalculateOrderTotalAsync(id);
            return Ok(total);
        }

        private List<string> GetModelStateErrors()
        {
            var errors = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
} 