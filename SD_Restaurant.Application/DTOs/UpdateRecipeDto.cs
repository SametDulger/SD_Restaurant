using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateRecipeDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public int IngredientId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
        
        public string? Unit { get; set; }
        
        public string? Notes { get; set; }
        public string? Instructions { get; set; }
    }
} 