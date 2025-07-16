using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        
        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        public decimal MinimumStock { get; set; } = 0;
        
        public string? Location { get; set; } // Depo, Bar, Mutfak vb.
        
        public decimal Cost { get; set; } // Birim maliyet
    }
} 