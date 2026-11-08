using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicSystem.Tests.Helpers
{
    internal static class ClaimsFactory
    {
        public static ControllerContext CreateContext(params Claim[] claims)
        {
            var identity = new ClaimsIdentity(claims, "AuthTest");

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(identity)
                }
            };
        }
    }
}
