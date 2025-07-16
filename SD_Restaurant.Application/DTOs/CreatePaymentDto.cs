using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreatePaymentDto
    {
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
        
        public string Currency { get; set; } = "₺";
        
        [MaxLength(100)]
        public string? TransactionId { get; set; }
        
        public string Status { get; set; } = "Beklemede";
        
        [MaxLength(500)]
        public string? Notes { get; set; }
    }
} 