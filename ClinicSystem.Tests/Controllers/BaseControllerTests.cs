using clinic_system.Exceptions;
using ClinicSystem.Tests.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClinicSystem.Tests.Controllers
{
    [TestFixture]
    internal class BaseControllerTests
    {
        private TestableController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new TestableController();
        }
        #region GetCurrentUserId()

        [Test]
        public void GetCurrentUserId_NameIdentifier_ReturnsId()
        {
            _controller.ControllerContext = ClaimsFactory.CreateContext
                (new Claim(ClaimTypes.NameIdentifier, "7"));

            var result = _controller.CallGetCurrentUserId();

            Assert.That(result, Is.EqualTo(7));
        }
        [Test]
        public void GetCurrentUserId_SubClaim_ReturnIdFromSub()
        {
            _controller.ControllerContext = ClaimsFactory.CreateContext(new Claim(JwtRegisteredClaimNames.Sub, "9"));

            var result = _controller.CallGetCurrentUserId();

            Assert.That(result, Is.EqualTo(9));
        }
        [Test]
        public void GetCurrentUserId_BothClaims_NameIdentifierHasPriority()
        {
            _controller.ControllerContext = ClaimsFactory.CreateContext(
                new Claim(ClaimTypes.NameIdentifier, "7"),
                new Claim(JwtRegisteredClaimNames.Sub, "9")
                );
            var result = _controller.CallGetCurrentUserId();

            Assert.That(result, Is.EqualTo(7));
        }

        [TestCase(null)]
        [TestCase("abc")]
        public void GetCurrentUserId_NoOrNonNumericClaims_throwUnauthorizedException(string? value)
        {
            if (value == null) _controller.ControllerContext = ClaimsFactory.CreateContext();
            else _controller.ControllerContext = ClaimsFactory.CreateContext(new Claim(ClaimTypes.NameIdentifier, value));

            Assert.That(() => _controller.CallGetCurrentUserId(),
                Throws.TypeOf<UnauthorizedException>()
                .With.Message.EqualTo("Clinic ID not found in token claims"));
        }
        #endregion

        #region ValidateModel()

        [Test]
        public void ValidModel_ValidModelState_ReturnNothing()
        {
            Assert.That(() => _controller.CallValidateModel(),
                Throws.Nothing);
        }
        [Test]
        public void ValidModel_InValidModelState_ReturnValidationException()
        {
            _controller.ModelState.AddModelError("Email", "Email Is Required");
            Assert.That(() => _controller.CallValidateModel(),
                Throws.TypeOf<ValidationException>());
        }
        #endregion
    }
}

