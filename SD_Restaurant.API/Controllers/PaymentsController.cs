using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

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
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByOrder(int orderId)
        {
            var payments = await _paymentService.GetPaymentsByOrderAsync(orderId);
            return Ok(payments);
        }

        [HttpGet("method/{paymentMethod}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByMethod(string paymentMethod)
        {
            var payments = await _paymentService.GetPaymentsByMethodAsync(paymentMethod);
            return Ok(payments);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var payments = await _paymentService.GetPaymentsByDateRangeAsync(startDate, endDate);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPayment = await _paymentService.CreatePaymentAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto updatePaymentDto)
        {
            if (id != updatePaymentDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _paymentService.UpdatePaymentAsync(updatePaymentDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 