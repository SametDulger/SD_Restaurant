using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }
        
        public string? Description { get; set; }
    }
} 