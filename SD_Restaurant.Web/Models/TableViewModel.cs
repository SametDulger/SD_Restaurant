using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class TableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Masa numarası zorunludur")]
        [Range(1, 100, ErrorMessage = "Masa numarası 1-100 arasında olmalıdır")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Kapasite zorunludur")]
        [Range(1, 20, ErrorMessage = "Kapasite 1-20 arasında olmalıdır")]
        public int Capacity { get; set; }

        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir")]
        public string? Status { get; set; }

        [StringLength(200, ErrorMessage = "Konum en fazla 200 karakter olabilir")]
        public string? Location { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 