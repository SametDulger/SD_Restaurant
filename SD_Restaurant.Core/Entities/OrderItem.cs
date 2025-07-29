using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        
        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        
        [MaxLength(200)]
        public string? SpecialInstructions { get; set; }
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
} 