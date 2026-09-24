using System.ComponentModel.DataAnnotations;

namespace clinic_system.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Logo { get; set; }
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    }
}
