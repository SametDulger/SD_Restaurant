using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; } = string.Empty; // Nakit, Kredi Kartı, Banka Kartı
        
        public string Currency { get; set; } = "₺";
        
        public DateTime PaymentDate { get; set; }
        
        [MaxLength(100)]
        public string? TransactionId { get; set; }
        
        public string? Status { get; set; } // Başarılı, Başarısız, Beklemede
        
        [MaxLength(500)]
        public string? Notes { get; set; }
    }
} 