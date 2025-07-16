using SD_Restaurant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<Customer?> GetCustomerByPhoneAsync(string phone);
        Task<Customer?> GetCustomerWithOrdersAsync(int customerId);
        Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    }
} 