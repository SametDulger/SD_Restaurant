using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CustomerDto>>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(ApiResponse<IEnumerable<CustomerDto>>.SuccessResult(customers, "Müşteriler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound(ApiResponse<CustomerDto>.ErrorResult("Müşteri bulunamadı"));
            }
            return Ok(ApiResponse<CustomerDto>.SuccessResult(customer, "Müşteri başarıyla getirildi"));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CustomerDto>>>> SearchCustomers([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(ApiResponse<IEnumerable<CustomerDto>>.ErrorResult("Arama terimi gerekli"));
            }

            var customers = await _customerService.SearchCustomersAsync(term);
            return Ok(ApiResponse<IEnumerable<CustomerDto>>.SuccessResult(customers, "Arama sonuçları"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<CustomerDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdCustomer = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, 
                ApiResponse<CustomerDto>.SuccessResult(createdCustomer, "Müşteri başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _customerService.UpdateCustomerAsync(updateCustomerDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Müşteri bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Müşteri başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Müşteri bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Müşteri başarıyla silindi"));
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