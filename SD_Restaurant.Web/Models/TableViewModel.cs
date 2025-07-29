using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class TableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Masa numarası zorunludur")]
        [StringLength(20, ErrorMessage = "Masa numarası en fazla 20 karakter olabilir")]
        public string TableNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kapasite zorunludur")]
        [Range(1, 20, ErrorMessage = "Kapasite 1-20 arasında olmalıdır")]
        public int Capacity { get; set; }

        public string? Status { get; set; }

        [StringLength(200, ErrorMessage = "Konum en fazla 200 karakter olabilir")]
        public string? Location { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 