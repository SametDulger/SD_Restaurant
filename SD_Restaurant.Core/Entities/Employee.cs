using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Employee : BaseEntity
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
        
        [Required]
        public string Position { get; set; } = string.Empty; // Garson, Şef, Kasiyer, Yönetici
        
        public string? Role { get; set; } // Garson, Şef, Kasiyer, Yönetici
        
        public decimal Salary { get; set; }
        
        public string? Department { get; set; } // Mutfak, Servis, Yönetim
        
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
} 