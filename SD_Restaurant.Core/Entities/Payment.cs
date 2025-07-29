using System;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        
        public string Currency { get; set; } = "₺";
        
        public DateTime PaymentDate { get; set; }
        
        [MaxLength(100)]
        public string? TransactionId { get; set; }
        
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        
        [MaxLength(500)]
        public string? Notes { get; set; }
    }
} 