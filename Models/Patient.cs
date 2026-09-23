using clinic_system.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace clinic_system.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
        public int Age { get; set; }
        public string? Illness { get; set; }
        public Gender Gender { get; set; }

        // Navigation property for the related Clinic

        public int ClinicId { get; set; }
        public Clinic? Clinic { get; set; }


    }
}
