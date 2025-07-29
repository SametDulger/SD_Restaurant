using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD_Restaurant.Core.Enums;

namespace SD_Restaurant.Core.Entities
{
    public class Table : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string TableNumber { get; set; } = string.Empty;
        
        public int Capacity { get; set; }
        
        public TableStatus Status { get; set; } = TableStatus.Available;
        
        public string? Location { get; set; } // İç mekan, Dış mekan, VIP vb.
        
        public bool IsAvailable { get; set; } = true;
        
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
} 