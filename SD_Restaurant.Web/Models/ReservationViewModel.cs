using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Müşteri ID zorunludur")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Masa ID zorunludur")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Rezervasyon tarihi zorunludur")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Kişi sayısı zorunludur")]
        [Range(1, 20, ErrorMessage = "Kişi sayısı 1-20 arasında olmalıdır")]
        public int NumberOfGuests { get; set; }

        public string? Status { get; set; }

        [StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir")]
        public string? Notes { get; set; }

        public string? CustomerName { get; set; }
        public string? TableNumber { get; set; }
        public string? CustomerPhone { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public string? SpecialRequests { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 