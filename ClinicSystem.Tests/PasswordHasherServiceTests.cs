using clinic_system.Helpers;

namespace ClinicSystem.Tests
{
    [TestFixture]
    internal class PasswordHasherServiceTests
    {

        [Test]
        public void HashValidPassword_ReturnDifferentFromPlaintext()
        {
            var password = "rawan123";
            var hash = PasswordHasherService.HashPassword(password);
            
            Assert.That(hash , Is.Not.Null.And.Not.Empty);
            Assert.That(hash, Is.Not.EqualTo(password));

        }
        [Test]
        public void HashPassword_SamePasswordTwice_ReturnDifferentHashes()
        {
            var hash1 = PasswordHasherService.HashPassword("rawan123");
            var hash2 = PasswordHasherService.HashPassword("rawan123");

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }
        [Test]
        public void VerifyPassword_correctPassword_returnTrue()
        {
            var password = "CorrectPass";
            var hash = PasswordHasherService.HashPassword(password);

            var result = PasswordHasherService.VerifyPassword(password , hash);

            Assert.That(result, Is.True);
        }
        [Test]
        public void VerifyPassword_WrongPassword_returnFalse()
        {
            var password = "rawan123";
            var hash = PasswordHasherService.HashPassword(password);

            var result = PasswordHasherService.VerifyPassword("WrongPass", hash);

            Assert.That(result, Is.False);
        }
    }
}
