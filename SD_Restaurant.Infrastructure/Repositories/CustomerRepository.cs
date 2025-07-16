using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<Customer?> GetCustomerWithOrdersAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
        {
            return await _context.Customers
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
} 