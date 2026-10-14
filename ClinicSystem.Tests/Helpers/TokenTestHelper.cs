using clinic_system.Controllers;
using clinic_system.DTOs.AuthDTOs;
using clinic_system.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ClinicSystem.Tests.Helpers
{
    internal static class TokenTestHelper
    {
        //Reflection
        public static object? GetProp(object? obj, string name)
            => obj?.GetType().GetProperty(name)?.GetValue(obj);

        public static async Task<JwtSecurityToken> LoginAndReadToken(
            Context context,
            AuthController controller,
            string email = "t@test.com",
            string password = TestDataSeeders.DefaultPassword
            )
        {
            await TestDataSeeders.AddClinic(context, email: email, password: password);

            var result = await controller.Login(new LoginDTO
            {
                Email = email,
                Password = password
            });
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var value = ((OkObjectResult)result).Value;//new { clinic.ClinicId, clinic.Name, ..., token }
            var token = GetProp(value, "token") as string;

            Assert.That(token, Is.Not.Null.And.Not.Empty);
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
    }
}
