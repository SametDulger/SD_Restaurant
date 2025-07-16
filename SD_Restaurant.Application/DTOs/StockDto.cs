using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal MinimumStock { get; set; }
        public string? Location { get; set; }
        public decimal Cost { get; set; }
    }
} 