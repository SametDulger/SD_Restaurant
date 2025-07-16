using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByPositionAsync(string position);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(string department);
        Task<EmployeeDto> GetEmployeeByEmailAsync(string email);
        Task<EmployeeDto> GetEmployeeByPhoneAsync(string phone);
        Task<IEnumerable<EmployeeDto>> GetActiveEmployeesAsync();
        Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(string role);
    }
} 