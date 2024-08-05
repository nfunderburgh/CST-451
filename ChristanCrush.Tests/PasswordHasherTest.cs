using ChristanCrush.Utility;
using Xunit;

namespace ChristanCrush.Tests
{
    public class PasswordHasherTest
    {

        [Fact]
        public void HashPassword()
        {

            var passwordHasher = new PasswordHasher();
            string password = "securepassword";

            string hashedPassword = passwordHasher.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.NotEqual(password, hashedPassword);
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            Assert.Equal(48, hashedPasswordBytes.Length);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue()
        {
            var passwordHasher = new PasswordHasher();
            string password = "securepassword";
            string hashedPassword = passwordHasher.HashPassword(password);

            bool result = passwordHasher.VerifyPassword(password, hashedPassword);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse()
        {
            var passwordHasher = new PasswordHasher();
            string password = "securepassword";
            string hashedPassword = passwordHasher.HashPassword(password);
            string incorrectPassword = "wrongpassword";

            bool result = passwordHasher.VerifyPassword(incorrectPassword, hashedPassword);

            Assert.False(result);
        }
    }
}