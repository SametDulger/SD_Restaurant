using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateTableDto
    {
        public int Id { get; set; }
        public string? TableNumber { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }
        
        public string? Status { get; set; }
        
        public string? Location { get; set; }
        
        public bool IsAvailable { get; set; }
    }
} 