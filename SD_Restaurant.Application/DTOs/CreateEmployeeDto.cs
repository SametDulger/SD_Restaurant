using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateEmployeeDto
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Position { get; set; }
        
        public string? Role { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
        
        public string? Department { get; set; }
    }
} 