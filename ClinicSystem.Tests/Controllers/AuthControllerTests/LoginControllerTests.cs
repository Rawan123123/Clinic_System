using clinic_system.Controllers;
using clinic_system.DTOs.AuthDTOs;
using clinic_system.Models;
using ClinicSystem.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Tests.Controllers.AuthControllerTests
{
    [TestFixture]
    internal class LoginControllerTests : AuthTestBase
    {

        [Test]
        public async Task LoginValidCredentials_ReturnOkWithToken()
        {
            await TestDataSeeders.AddClinic(context : _context , email: "t@test.com");
            var dto = new LoginDTO
            {
                Email = "t@test.com",
                Password = TestDataSeeders.DefaultPassword
            };
            var result = await _controller.Login(dto);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var value = ((OkObjectResult)result).Value;
            Assert.That(TokenTestHelper.GetProp(value, "token") as string, Is.Not.Null.And.Not.Empty);

        }
        [Test]
        public async Task Login_UnknownEmail_ReturnsUnauthorized()
        {
            var dto = new LoginDTO
            {
                Email = "non@test.com",
                Password = TestDataSeeders.DefaultPassword
            };

            var result = await _controller.Login(dto);

            Assert.That(result, Is.TypeOf<UnauthorizedObjectResult>());
        }
        [Test]
        public async Task Login_WrongPassword_ReturnsUnauthorized()
        {
            await TestDataSeeders.AddClinic(_context, email: "a@test.com");

            var dto = new LoginDTO
            {
                Email = "a@test.com",
                Password = "WrongPass"
            };

            var result = await _controller.Login(dto);

            Assert.That(result, Is.TypeOf<UnauthorizedObjectResult>());
        }
        [Test]
        public async Task Login_EmailWithSpacesAndUpperCase_StillSucceeds()
        {
            await TestDataSeeders.AddClinic(_context, email: "a@test.com");
            var dto = new LoginDTO
            {
                Email = "  A@TeST.com ",
                Password = TestDataSeeders.DefaultPassword
            };

            var result = await _controller.Login(dto);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public async Task Login_ValidCredentials_TokenHasCorrectIssuerAndAudience()
        {
            var jwt =await TokenTestHelper.LoginAndReadToken(_context, _controller);
            Assert.That(jwt.Issuer, Is.EqualTo("TestIssuer"));
            Assert.That(jwt.Audiences, Does.Contain("TestAudience"));
        }
        [Test]
        public async Task Login_ValidCredentials_TokenContainsClinicId()
        {
            var clinic = await TestDataSeeders.AddClinic(_context, email: "t@test.com");

            var jwt = await TokenTestHelper.LoginAndReadToken(
                _context, _controller, email: "t@test.com");

            Assert.That(jwt.Claims.Any(c => c.Value == clinic.ClinicId.ToString()), Is.True);
        }

    }
}
