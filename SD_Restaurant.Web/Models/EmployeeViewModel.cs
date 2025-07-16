using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pozisyon zorunludur")]
        [StringLength(100, ErrorMessage = "Pozisyon en fazla 100 karakter olabilir")]
        public string Position { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Maaş zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş pozitif bir değer olmalıdır")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "İşe başlama tarihi zorunludur")]
        public DateTime HireDate { get; set; }

        public string? Role { get; set; }
        public string? Department { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
} 