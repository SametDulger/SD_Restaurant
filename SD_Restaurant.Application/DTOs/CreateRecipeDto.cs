using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateRecipeDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public int IngredientId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "Unit cannot exceed 20 characters")]
        public string Unit { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Instructions cannot exceed 200 characters")]
        public string? Instructions { get; set; }
    }
} 