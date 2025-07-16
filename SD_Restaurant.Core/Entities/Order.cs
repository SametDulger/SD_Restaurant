using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public string OrderNumber { get; set; } = string.Empty;
        
        public int TableId { get; set; }
        public virtual Table? Table { get; set; }
        
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        
        public DateTime OrderDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        
        public string? Status { get; set; } // Beklemede, Hazırlanıyor, Tamamlandı, İptal
        
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        
        public string Currency { get; set; } = "₺";
        
        [MaxLength(500)]
        public string? Notes { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
} 