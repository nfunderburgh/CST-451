using ChristanCrush.Utility;
using Xunit;

namespace ChristanCrush.Tests
{
    public class PasswordHasherTest
    {
        /// <summary>
        /// The HashPassword function tests the hashing of a password using a PasswordHasher class in
        /// C#.
        /// </summary>
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

        /// <summary>
        /// The function verifies a password by hashing it and then comparing it with the hashed
        /// password.
        /// </summary>
        [Fact]
        public void VerifyPassword()
        {
            var passwordHasher = new PasswordHasher();
            string password = "securepassword";
            string hashedPassword = passwordHasher.HashPassword(password);

            bool result = passwordHasher.VerifyPassword(password, hashedPassword);

            Assert.True(result);
        }

        /// <summary>
        /// The function VerifyPasswordWrong tests the PasswordHasher class by verifying an incorrect
        /// password against a hashed password.
        /// </summary>
        [Fact]
        public void VerifyPasswordWrong()
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