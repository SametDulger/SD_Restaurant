using System.Collections.Generic;

namespace SD_Restaurant.Web.Models
{
    public class StockReportViewModel
    {
        public List<StockViewModel> Stocks { get; set; } = new List<StockViewModel>();
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public int HealthyStockItems => TotalItems - LowStockItems - OutOfStockItems;
        public decimal TotalStockValue => Stocks.Sum(s => s.Quantity * s.UnitPrice);
    }
} 