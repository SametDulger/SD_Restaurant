using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<bool> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string term);
        Task<CustomerDto?> GetCustomerByEmailAsync(string email);
        Task<CustomerDto?> GetCustomerByPhoneAsync(string phone);
        Task<IEnumerable<CustomerDto>> GetActiveCustomersAsync();
    }
} 