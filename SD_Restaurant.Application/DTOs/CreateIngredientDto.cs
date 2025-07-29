using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateIngredientDto
    {
        [Required(ErrorMessage = "Malzeme adı zorunludur")]
        [MaxLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Birim zorunludur")]
        [MaxLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        public string Unit { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "Maliyet pozitif bir değer olmalıdır")]
        public decimal Cost { get; set; }
    }
} 