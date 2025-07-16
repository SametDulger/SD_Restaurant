using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Description { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
} 