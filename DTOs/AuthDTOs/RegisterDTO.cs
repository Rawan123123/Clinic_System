using System.ComponentModel.DataAnnotations;

namespace clinic_system.DTOs.AuthDTOs
{
    public class RegisterDTO
    {

        [Required , MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required , EmailAddress , MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8) , MaxLength(72)]
        public string Password { get; set; } = string.Empty;

        [Phone, MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

    }
}
