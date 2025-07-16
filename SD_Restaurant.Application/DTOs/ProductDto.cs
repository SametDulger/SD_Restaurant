using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        
        public bool IsRecipe { get; set; }
        
        public decimal CurrentStock { get; set; }
        public string? StockLocation { get; set; }
    }
} 