using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string? TableNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public string? Status { get; set; }
        public string? SpecialRequests { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 