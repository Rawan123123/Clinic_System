using clinic_system.Controllers.Base;
using clinic_system.DTOs.AuthDTOs;
using clinic_system.Helpers;
using clinic_system.Models;
using clinic_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace clinic_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly Context _context;
        private readonly JWTService _jwtService;

        public AuthController(Context context , JWTService jWTService)
        {
            _context = context;
            _jwtService = jWTService;
        }


        [HttpPost("Register")]
        public  async Task<IActionResult> Register(RegisterDTO RequestDTO)
        {
            ValidateModel();
            var email = RequestDTO.Email.Trim().ToLowerInvariant();

            Clinic exist = await _context.Clinics.FirstOrDefaultAsync(c => c.Email == email);
            if(exist != null)
            {
                return BadRequest(new {message = "Clinic with this email already exists." });

            }
            string hashed = PasswordHasherService.HashPassword(RequestDTO.Password);
            Clinic clinic = new Clinic()
            {
                Name = RequestDTO.Name,
                Email = email,
                PasswordHash = hashed,
                PhoneNumber = RequestDTO.PhoneNumber,
                Address =   RequestDTO.Address,
                Logo = "default.png"

            };
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
            return Ok(new {clinic.ClinicId ,clinic.Name , clinic.Email , clinic.PhoneNumber , clinic.Logo});

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO LoginDTO)
        {
            ValidateModel();
            var email = LoginDTO.Email.Trim().ToLowerInvariant(); 
            Clinic clinic =await _context.Clinics.FirstOrDefaultAsync(c => c.Email == email);
            if(clinic == null)
            {
                return Unauthorized(new {message = "Invalid email or password." });
            }

            bool isValidPassword = PasswordHasherService.VerifyPassword(LoginDTO.Password, clinic.PasswordHash);
            if (!isValidPassword)
            {
                return Unauthorized(new {message = "Invalid email or password." });
            }
            string token = _jwtService.CreateToken(clinic);
            return Ok(new { clinic.ClinicId, clinic.Name, clinic.Email, clinic.PhoneNumber, clinic.Logo, token});

        }


    }
}
