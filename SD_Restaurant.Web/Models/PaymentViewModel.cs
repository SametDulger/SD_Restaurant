using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sipariş ID zorunludur")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Ödeme tutarı zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Ödeme tutarı pozitif bir değer olmalıdır")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Ödeme yöntemi zorunludur")]
        [StringLength(50, ErrorMessage = "Ödeme yöntemi en fazla 50 karakter olabilir")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ödeme tarihi zorunludur")]
        public DateTime PaymentDate { get; set; }

        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir")]
        public string? Status { get; set; }

        [StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir")]
        public string? Notes { get; set; }

        public string? OrderNumber { get; set; }
    }
} 