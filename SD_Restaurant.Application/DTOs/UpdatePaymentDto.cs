using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdatePaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Currency { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Notes { get; set; }
    }
} 