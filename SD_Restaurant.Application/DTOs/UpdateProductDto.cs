using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        [Required]
        public int CategoryId { get; set; }
        
        public bool IsRecipe { get; set; }
    }
} 