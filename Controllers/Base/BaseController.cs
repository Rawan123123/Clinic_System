using clinic_system.Exceptions;
using clinic_system.Exeptions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace clinic_system.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                throw new ValidationException("Validation Failed" ,  errors );
            }
        }

        //create method to get Id of logged in user from JWT token
        protected int GetCurrentUserId()
        {
            var subClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(subClaim))
            {
                throw new UnauthorizedException("Clinic ID not found in token claims");
            }
            return int.Parse(subClaim);
        }
    }
}
