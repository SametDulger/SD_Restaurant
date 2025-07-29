using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Müşteri ID zorunludur")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Masa ID zorunludur")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Sipariş tarihi zorunludur")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Toplam tutar zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Toplam tutar pozitif bir değer olmalıdır")]
        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        [StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir")]
        public string? Notes { get; set; }

        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? TableNumber { get; set; }
        public string? EmployeeName { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string? Currency { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
        
        // Added missing properties
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public string? SpecialInstructions { get; set; }
        public string? Status { get; set; }
    }
} 