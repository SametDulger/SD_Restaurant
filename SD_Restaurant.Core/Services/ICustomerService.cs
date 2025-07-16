using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> GetCustomerByPhoneAsync(string phone);
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    }
} 