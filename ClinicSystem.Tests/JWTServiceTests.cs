using clinic_system.Models;
using clinic_system.Services;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;


namespace ClinicSystem.Tests
{
    [TestFixture]
    internal class JWTServiceTests
    {
        private const string ValidSecret = "ThisIsAVerySecretKeyWith32Chars!AtLeast";
        private Clinic _clinic;

        [SetUp]
        public void SetUp()
        {
            _clinic = new Clinic { ClinicId = 3, Email = "clinic@test.com" };
        }

        private static IConfiguration BuildConfig(string? secret)
        {
            var setting = new Dictionary<string, string?>()
            {
                ["JWT:Issuer"] = "TestIssuer",
                ["JWT:Audience"] = "TestAudience"
            };
            if (secret != null)
            {
                setting["JWT:SecretKey"] = secret;
            }
            return new ConfigurationBuilder()
                .AddInMemoryCollection(setting).Build();
        }           
        private static JwtSecurityToken Decode(string token)
           => new JwtSecurityTokenHandler().ReadJwtToken(token);

        [Test]
        public void CreateToken_ValidConfig_ReturnNonEmptyToken()
        {
            var Service = new JWTService(BuildConfig(ValidSecret));
            var token = Service.CreateToken(_clinic);

            Assert.That(token, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void CreateToken_ValidConfig_ContainsClinicIdAndEmailClaims()
        {
            var Service = new JWTService(BuildConfig(ValidSecret));
            var jwt = Decode(Service.CreateToken(_clinic));

            Assert.That(jwt.Subject, Is.EqualTo("3"));
            Assert.That(jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value,
                Is.EqualTo("clinic@test.com"));
        }

        [Test]
        public void CreateToken_ValidConfig_HasIssuerAndAudienceFromConfig()
        {
            var Service = new JWTService(BuildConfig(ValidSecret));
            var jwt = Decode(Service.CreateToken(_clinic));

            Assert.That(jwt.Issuer, Is.EqualTo("TestIssuer"));
            Assert.That(jwt.Audiences, Does.Contain("TestAudience"));
        }

        [Test]
        public void CreateToken_ValidConfig_ExpiresInAbout24Hours()
        {
            var Service = new JWTService(BuildConfig(ValidSecret));
            var jwt = Decode(Service.CreateToken(_clinic));

            var expected = DateTime.UtcNow.AddHours(24);

            Assert.That(jwt.ValidTo, Is.EqualTo(expected).Within(1).Minutes);
        }

        [Test]
        public void CreateToken_MissingSecretKey_ReturnException()
        {
            var Service = new JWTService(BuildConfig(null));
            Assert.That(() => Service.CreateToken(_clinic) ,
                Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo("JWT:SecretKey is missing.")
                );

        }
        [Test]
        public void CreateToken_ShortSecretKey_ReturnException()
        {
            var Service = new JWTService(BuildConfig("ShortSecretKey"));
            Assert.That(() => Service.CreateToken(_clinic),
                Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo("JWT:SecretKey must be at least 32 bytes.")
                );

        }

    }
}
