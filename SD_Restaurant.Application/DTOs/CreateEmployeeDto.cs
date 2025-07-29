using System.ComponentModel.DataAnnotations;

namespace SD_Restaurant.Application.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Ad zorunludur")]
        [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad zorunludur")]
        [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [MaxLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir")]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(20, ErrorMessage = "Telefon en fazla 20 karakter olabilir")]
        public string? Phone { get; set; }
        
        [Required(ErrorMessage = "Pozisyon zorunludur")]
        [MaxLength(50, ErrorMessage = "Pozisyon en fazla 50 karakter olabilir")]
        public string Position { get; set; } = string.Empty;
        
        [MaxLength(50, ErrorMessage = "Rol en fazla 50 karakter olabilir")]
        public string? Role { get; set; }
        
        [Required(ErrorMessage = "Maaş zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş pozitif bir değer olmalıdır")]
        public decimal Salary { get; set; }
        
        [MaxLength(50, ErrorMessage = "Departman en fazla 50 karakter olabilir")]
        public string? Department { get; set; }
    }
} 