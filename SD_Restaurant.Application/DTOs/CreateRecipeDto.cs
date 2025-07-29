using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateRecipeDto
    {
        [Required(ErrorMessage = "Ürün seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ürün seçilmelidir")]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Malzeme seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir malzeme seçilmelidir")]
        public int IngredientId { get; set; }
        
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalıdır")]
        public decimal Quantity { get; set; }
        
        [Required(ErrorMessage = "Birim zorunludur")]
        [MaxLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        public string Unit { get; set; } = string.Empty;
        
        [MaxLength(200, ErrorMessage = "Talimatlar en fazla 200 karakter olabilir")]
        public string? Instructions { get; set; }
    }
} 