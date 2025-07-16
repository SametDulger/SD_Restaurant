using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateReservationDto
    {
        [Required]
        public int TableId { get; set; }
        
        public int? CustomerId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? CustomerPhone { get; set; }
        
        [Required]
        public DateTime ReservationDate { get; set; }
        
        [Required]
        public TimeSpan ReservationTime { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int GuestCount { get; set; }
        
        public string Status { get; set; } = "Beklemede";
        
        [MaxLength(500)]
        public string? SpecialRequests { get; set; }
    }
} 