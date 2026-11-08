using clinic_system.Helpers;
using clinic_system.Models;

namespace ClinicSystem.Tests.Helpers
{
    internal static class TestDataSeeders
    {
        public const string DefaultPassword = "password123";
        public static async Task<Clinic> AddClinic(
            Context context,
            string email = "test@example.com",
            string password = DefaultPassword,
            string name = "Test Clinic")
        {
             Clinic clinic = new Clinic
             {
                 Email = email,
                 PasswordHash = PasswordHasherService.HashPassword(password),
                 Name = name
             };
            context.Clinics.Add(clinic);
            await context.SaveChangesAsync();
            return clinic;

        }


    }
}
