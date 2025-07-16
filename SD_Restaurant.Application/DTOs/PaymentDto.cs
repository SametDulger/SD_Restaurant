using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 