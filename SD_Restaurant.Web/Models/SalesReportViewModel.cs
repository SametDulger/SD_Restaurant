using System;
using System.Collections.Generic;

namespace SD_Restaurant.Web.Models
{
    public class SalesReportViewModel
    {
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue => TotalOrders > 0 ? TotalRevenue / TotalOrders : 0;
        public int CompletedOrders => Orders.Count(o => o.Status == "Completed");
        public int PendingOrders => Orders.Count(o => o.Status == "Pending");
        public int CancelledOrders => Orders.Count(o => o.Status == "Cancelled");
    }
} 