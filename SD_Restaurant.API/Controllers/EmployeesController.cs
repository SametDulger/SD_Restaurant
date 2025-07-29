using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeDto>>.SuccessResult(employees, "Personel başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDto>.ErrorResult("Personel bulunamadı"));
            }
            return Ok(ApiResponse<EmployeeDto>.SuccessResult(employee, "Personel başarıyla getirildi"));
        }

        [HttpGet("position/{position}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> GetEmployeesByPosition(string position)
        {
            var employees = await _employeeService.GetEmployeesByPositionAsync(position);
            return Ok(ApiResponse<IEnumerable<EmployeeDto>>.SuccessResult(employees, "Pozisyon bazlı personel getirildi"));
        }

        [HttpGet("department/{department}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> GetEmployeesByDepartment(string department)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentAsync(department);
            return Ok(ApiResponse<IEnumerable<EmployeeDto>>.SuccessResult(employees, "Departman bazlı personel getirildi"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<EmployeeDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, 
                ApiResponse<EmployeeDto>.SuccessResult(createdEmployee, "Personel başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _employeeService.UpdateEmployeeAsync(updateEmployeeDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Personel bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Personel başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Personel bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Personel başarıyla silindi"));
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