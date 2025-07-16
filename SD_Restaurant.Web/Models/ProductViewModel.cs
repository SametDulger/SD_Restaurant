using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Mevcut mu?")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Birim")]
        public string? Unit { get; set; }

        // Navigation properties for display
        [Display(Name = "Kategori Adı")]
        public string? CategoryName { get; set; }

        public string? StockLocation { get; set; }
        public bool IsRecipe { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 