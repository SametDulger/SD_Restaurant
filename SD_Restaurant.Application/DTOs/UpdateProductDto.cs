using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [MaxLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat pozitif bir değer olmalıdır")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Birim zorunludur")]
        [MaxLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        public string Unit { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kategori seçilmelidir")]
        public int CategoryId { get; set; }
        
        public bool IsRecipe { get; set; }
    }
} 