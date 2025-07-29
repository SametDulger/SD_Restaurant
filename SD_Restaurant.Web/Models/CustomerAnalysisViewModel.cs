using System.Collections.Generic;

namespace SD_Restaurant.Web.Models
{
    public class CustomerAnalysisViewModel
    {
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public int TotalCustomers { get; set; }
        public int NewCustomersThisMonth { get; set; }
        public int RegularCustomers => Customers.Count(c => c.CustomerType == "Regular");
        public int VIPCustomers => Customers.Count(c => c.CustomerType == "VIP");
        public int CorporateCustomers => Customers.Count(c => c.CustomerType == "Corporate");
    }
} 