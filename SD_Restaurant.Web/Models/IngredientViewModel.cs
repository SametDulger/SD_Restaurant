using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Malzeme adı zorunludur")]
        [MaxLength(100, ErrorMessage = "Malzeme adı en fazla 100 karakter olabilir")]
        [Display(Name = "Malzeme Adı")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Birim zorunludur")]
        [MaxLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        [Display(Name = "Birim")]
        public string Unit { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Maliyet pozitif bir değer olmalıdır")]
        [Display(Name = "Maliyet")]
        public decimal Cost { get; set; }

        [Display(Name = "Mevcut Stok")]
        public decimal CurrentStock { get; set; }

        [Display(Name = "Stok Lokasyonu")]
        public string? StockLocation { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
} 