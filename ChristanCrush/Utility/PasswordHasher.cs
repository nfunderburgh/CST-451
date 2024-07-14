using System.Security.Cryptography;
using System.Text;

namespace ChristanCrush.Utility
{
    public class PasswordHasher
    {

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, combinedBytes, passwordBytes.Length);
                Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                byte[] hashedPasswordBytes = new byte[hashedBytes.Length + salt.Length];
                Array.Copy(hashedBytes, hashedPasswordBytes, hashedBytes.Length);
                Array.Copy(salt, 0, hashedPasswordBytes, hashedBytes.Length, salt.Length);

                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];

            Array.Copy(hashedPasswordBytes, hashedPasswordBytes.Length - salt.Length, salt, 0, salt.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, combinedBytes, passwordBytes.Length);
                Array.Copy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    if (hashedBytes[i] != hashedPasswordBytes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
