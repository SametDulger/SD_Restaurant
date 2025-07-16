using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün ID zorunludur")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Malzeme adı zorunludur")]
        [StringLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir")]
        public string IngredientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Miktar pozitif bir değer olmalıdır")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Birim zorunludur")]
        [StringLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        public string Unit { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Instructions { get; set; }

        public string? ProductName { get; set; }
    }
} 