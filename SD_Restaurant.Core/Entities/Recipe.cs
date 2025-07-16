using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        
        public int IngredientId { get; set; }
        public virtual Product? Ingredient { get; set; }
        
        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Instructions { get; set; }
    }
} 