using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class TableDto
    {
        public int Id { get; set; }
        public string? TableNumber { get; set; }
        public int Capacity { get; set; }
        public string? Status { get; set; }
        public string? Location { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 