using clinic_system.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clinic_system.Services
{
    public class JWTService
    {
        private readonly IConfiguration _config;

        public JWTService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(Clinic clinic)
        {
            List<Claim> ClinicClaim = new List<Claim>();

            ClinicClaim.Add(new Claim(JwtRegisteredClaimNames.Sub, clinic.ClinicId.ToString()));
            ClinicClaim.Add(new Claim(JwtRegisteredClaimNames.Email, clinic.Email));

            var secret = _config["JWT:SecretKey"]
                  ?? throw new InvalidOperationException("JWT:SecretKey is missing.");

            if (Encoding.UTF8.GetBytes(secret).Length < 32)
                throw new InvalidOperationException("JWT:SecretKey must be at least 32 bytes.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));


            SigningCredentials creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims:ClinicClaim,
                expires:DateTime.UtcNow.AddHours(24),
                signingCredentials:creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
