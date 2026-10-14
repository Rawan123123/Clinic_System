using clinic_system.Controllers;
using clinic_system.DTOs.AuthDTOs;
using clinic_system.Helpers;
using clinic_system.Models;
using ClinicSystem.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace ClinicSystem.Tests.Controllers.AuthControllerTests
{
    [TestFixture]
    internal class RegisterControllerTests : AuthTestBase
    {

        [Test]
        public async Task Register_ExistingEmail_ReturnBadRequestAndDoesNotDuplicate()
        {
            var dto = new RegisterDTO
            {
                Email = "Test@example.com",
                Name = "clinic",
                Password = TestDataSeeders.DefaultPassword
            };

            await TestDataSeeders.AddClinic(_context, email : "test@example.com");

            var result = await _controller.Register(dto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(_context.Clinics.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task Register_NewEmail_ReturnsOkAndSavesClinic()
        {
            var dto = new RegisterDTO
            {
                Email = "a@tests.com",
                Name = "clinic",
                Password = TestDataSeeders.DefaultPassword
            };
            var result = await _controller.Register(dto);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(_context.Clinics.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task Register_EmailWithSpacesAndUpperCase_NormalizesEmail()
        {
            var dto = new RegisterDTO
            {
                Email = " A@TEST.com ",
                Name = "clinic",
                Password = TestDataSeeders.DefaultPassword
            };
            await _controller.Register(dto);

            Assert.That(_context.Clinics.Single().Email, Is.EqualTo("a@test.com"));
        }
        [Test]
        public async Task Register_NewEmail_StoresHashedPassword()
        {
            var dto = new RegisterDTO
            {
                Email = "a@test.com",
                Name = "clinic",
                Password = TestDataSeeders.DefaultPassword //password123
            };

            await _controller.Register(dto); // password is hashed

            var saved = _context.Clinics.Single();
            Assert.That(saved.PasswordHash, Is.Not.EqualTo(TestDataSeeders.DefaultPassword));
            Assert.That(PasswordHasherService.
                VerifyPassword(TestDataSeeders.DefaultPassword, saved.PasswordHash) , Is.True);
        }
    }
}
   