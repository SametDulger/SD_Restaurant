using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateStockDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue)]
        public decimal MinimumStock { get; set; }
        
        public string? Location { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
} 