using System.Security.Cryptography;
using System.Text;

namespace ChristanCrush.Utility
{
    public class PasswordHasher
    {

        public string HashPassword(string password)
        {
            
            byte[] salt = Encoding.UTF8.GetBytes("SomeSalt");

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, combinedBytes, passwordBytes.Length);
                Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
