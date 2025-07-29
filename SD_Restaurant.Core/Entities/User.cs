using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public new bool IsActive { get; set; } = true;

        public DateTime LastLoginDate { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
} 