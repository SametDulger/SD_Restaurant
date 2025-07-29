using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
} 