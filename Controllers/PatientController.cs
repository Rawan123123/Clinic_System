using clinic_system.Controllers.Base;
using clinic_system.DTOs.PatientDTO;
using clinic_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace clinic_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseController
    {
        private readonly Context _context;
        public PatientController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyPatients()
        {
            int ClinicId = GetCurrentUserId();

            var patients = await _context.Patients
                .Where(p => p.ClinicId == ClinicId)
                .Select(p => new ResponsePatientDTO
                {
                    PatientId = p.PatientId,
                    Name = p.Name,
                    Age = p.Age,
                    Illness = p.Illness,
                    Gender = p.Gender,
                }).ToListAsync();
            return Ok(patients);
        }
    }
}