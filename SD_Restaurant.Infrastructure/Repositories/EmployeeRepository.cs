using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee?> GetEmployeeByPhoneAsync(string phone)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Phone == phone);
        }

        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(string role)
        {
            return await _context.Employees
                .Where(e => e.Role == role && e.IsActive)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeWithOrdersAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }
    }
} 