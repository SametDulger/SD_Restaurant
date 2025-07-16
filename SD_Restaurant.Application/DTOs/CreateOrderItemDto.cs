using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
        
        public decimal DiscountAmount { get; set; }
        public string? SpecialInstructions { get; set; }
    }
} 