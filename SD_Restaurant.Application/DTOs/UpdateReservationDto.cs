using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int GuestCount { get; set; }
        public int NumberOfGuests { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? Status { get; set; }
        public string? SpecialRequests { get; set; }
    }
} 