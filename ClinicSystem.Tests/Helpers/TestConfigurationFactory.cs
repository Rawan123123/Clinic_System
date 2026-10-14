using Microsoft.Extensions.Configuration;

namespace ClinicSystem.Tests.Helpers
{
    internal static class TestConfigurationFactory
    {
        public const string ValidSecret = "ThisIsAVerySecretKeyWith32Chars!AtLeast";

        public static IConfiguration BuildConfig(string? secret = ValidSecret)
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
    }
}
