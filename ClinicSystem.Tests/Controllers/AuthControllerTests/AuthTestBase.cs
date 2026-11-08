using clinic_system.Controllers;
using clinic_system.Models;
using clinic_system.Services;
using ClinicSystem.Tests.Helpers;
using Microsoft.EntityFrameworkCore;


namespace ClinicSystem.Tests.Controllers.AuthControllerTests
{
    internal abstract class AuthTestBase
    {
        protected Context _context = null!;
        protected AuthController _controller = null!;

        [SetUp]
        public void Setup()
        {
            // Create a new in-memory database for each test
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);

            var config = TestConfigurationFactory.BuildConfig();

            _controller = new AuthController(_context, new JWTService(config));

        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
