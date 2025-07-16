using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateStockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }
        
        public string? Unit { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal MinimumStock { get; set; }
        
        public string? Location { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
} 