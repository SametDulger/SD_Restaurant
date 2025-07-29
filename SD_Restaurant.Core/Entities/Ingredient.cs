using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty; // kg, litre, adet, paket vb.
        
        public decimal Cost { get; set; }
        
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
} 