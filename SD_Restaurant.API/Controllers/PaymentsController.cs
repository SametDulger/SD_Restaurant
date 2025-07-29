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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(ApiResponse<IEnumerable<PaymentDto>>.SuccessResult(payments, "Ödemeler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PaymentDto>>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound(ApiResponse<PaymentDto>.ErrorResult("Ödeme bulunamadı"));
            }
            return Ok(ApiResponse<PaymentDto>.SuccessResult(payment, "Ödeme başarıyla getirildi"));
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetPaymentsByOrder(int orderId)
        {
            var payments = await _paymentService.GetPaymentsByOrderAsync(orderId);
            return Ok(ApiResponse<IEnumerable<PaymentDto>>.SuccessResult(payments, "Sipariş ödemeleri getirildi"));
        }

        [HttpGet("method/{paymentMethod}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetPaymentsByMethod(string paymentMethod)
        {
            if (Enum.TryParse<PaymentMethod>(paymentMethod, true, out var method))
            {
                var payments = await _paymentService.GetPaymentsByMethodAsync(method);
                return Ok(ApiResponse<IEnumerable<PaymentDto>>.SuccessResult(payments, "Yöntem bazlı ödemeler getirildi"));
            }
            return BadRequest(ApiResponse<IEnumerable<PaymentDto>>.ErrorResult("Geçersiz ödeme yöntemi"));
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var payments = await _paymentService.GetPaymentsByDateRangeAsync(startDate, endDate);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PaymentDto>>> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<PaymentDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdPayment = await _paymentService.CreatePaymentAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, 
                ApiResponse<PaymentDto>.SuccessResult(createdPayment, "Ödeme başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdatePayment(int id, UpdatePaymentDto updatePaymentDto)
        {
            if (id != updatePaymentDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _paymentService.UpdatePaymentAsync(updatePaymentDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Ödeme bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Ödeme başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Ödeme bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Ödeme başarıyla silindi"));
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