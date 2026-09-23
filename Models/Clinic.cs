using System.ComponentModel.DataAnnotations;

namespace clinic_system.Models
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Logo { get; set; }
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    }
}
