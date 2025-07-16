using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class StockViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün ID zorunludur")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Miktar pozitif bir değer olmalıdır")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Minimum stok seviyesi zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum stok seviyesi pozitif bir değer olmalıdır")]
        public decimal MinimumStockLevel { get; set; }

        [Required(ErrorMessage = "Son güncelleme tarihi zorunludur")]
        public DateTime LastUpdated { get; set; }

        [StringLength(200, ErrorMessage = "Konum en fazla 200 karakter olabilir")]
        public string? Location { get; set; }

        public string? ProductName { get; set; }
        public string? Unit { get; set; }
        public decimal MinimumStock { get; set; }
        public decimal MaximumStock { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 