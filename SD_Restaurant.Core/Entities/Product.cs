using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty; // kg, litre, adet, paket, bardak vb.
        
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        
        public bool IsRecipe { get; set; } = false; // Reçete gerektiren ürün mü?
        
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
} 