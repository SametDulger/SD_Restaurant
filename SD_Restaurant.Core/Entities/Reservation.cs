using System;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public int TableId { get; set; }
        public virtual Table? Table { get; set; }
        
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? CustomerPhone { get; set; }
        
        [Required]
        public DateTime ReservationDate { get; set; }
        
        [Required]
        public TimeSpan ReservationTime { get; set; }
        
        public int GuestCount { get; set; }
        
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        
        [MaxLength(500)]
        public string? SpecialRequests { get; set; }
    }
} 