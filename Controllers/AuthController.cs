using clinic_system.Controllers.Base;
using clinic_system.DTOs.AuthDTOs;
using clinic_system.Helpers;
using clinic_system.Models;
using clinic_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Register(RegisterDTO RequestDTO)
        {
            ValidateModel();
            var email = RequestDTO.Email.Trim().ToLowerInvariant();

            Clinic exist = _context.Clinics.FirstOrDefault(c => c.Email == email);
            if(exist != null)
            {
                return BadRequest("Clinic with this email already exists.");

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
            _context.SaveChanges();
            return Ok(new {clinic.ClinicId ,clinic.Name , clinic.Email , clinic.PhoneNumber , clinic.Logo});

        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO LoginDTO)
        {
            ValidateModel();
            var email = LoginDTO.Email.Trim().ToLowerInvariant(); 
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Email == email);
            if(clinic == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            bool isValidPassword = PasswordHasherService.VerifyPassword(LoginDTO.Password, clinic.PasswordHash);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password.");
            }
            string token = _jwtService.CreateToken(clinic);
            return Ok(new { clinic.ClinicId, clinic.Name, clinic.Email, clinic.PhoneNumber, clinic.Logo, token});

        }


    }
}
