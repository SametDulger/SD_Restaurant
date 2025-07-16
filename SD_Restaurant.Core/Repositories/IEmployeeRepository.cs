using SD_Restaurant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<Employee?> GetEmployeeByPhoneAsync(string phone);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(string role);
        Task<Employee?> GetEmployeeWithOrdersAsync(int employeeId);
    }
} 