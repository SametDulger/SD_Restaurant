using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Core.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        [MaxLength(20)]
        public string? Phone { get; set; }
        
        [MaxLength(200)]
        public string? Address { get; set; }
        
        public CustomerType CustomerType { get; set; } = CustomerType.Regular;
        
        public decimal TotalSpent { get; set; }
        public int VisitCount { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
} 