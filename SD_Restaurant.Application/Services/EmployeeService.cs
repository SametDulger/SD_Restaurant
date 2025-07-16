using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SD_Restaurant.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(createEmployeeDto);
            var createdEmployee = await _employeeRepository.AddAsync(employee);
            return _mapper.Map<EmployeeDto>(createdEmployee);
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(updateEmployeeDto.Id);
            if (existingEmployee == null)
                return false;

            _mapper.Map(updateEmployeeDto, existingEmployee);
            await _employeeRepository.UpdateAsync(existingEmployee);
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return false;

            await _employeeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByPositionAsync(string position)
        {
            var employees = await _employeeRepository.FindAsync(e => e.Position == position);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(string department)
        {
            var employees = await _employeeRepository.FindAsync(e => e.Department == department);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetEmployeeByPhoneAsync(string phone)
        {
            var employee = await _employeeRepository.GetEmployeeByPhoneAsync(phone);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetActiveEmployeesAsync()
        {
            var employees = await _employeeRepository.GetActiveEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByRoleAsync(string role)
        {
            var employees = await _employeeRepository.GetEmployeesByRoleAsync(role);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
} 