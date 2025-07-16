using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateTableDto
    {
        [Required]
        [MaxLength(20)]
        public string TableNumber { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }
        
        public string Status { get; set; } = "Boş";
        
        public string? Location { get; set; }
        
        public bool IsAvailable { get; set; } = true;
    }
} 