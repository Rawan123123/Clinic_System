using clinic_system.Enum;
using System.ComponentModel.DataAnnotations;

namespace clinic_system.DTOs.PatientDTO
{
    public class ResponsePatientDTO
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Illness { get; set; }
        public Gender Gender { get; set; }

    }
}
