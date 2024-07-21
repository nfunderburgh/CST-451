using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ChristanCrush.Utility
{
    public class PasswordValidation : ValidationAttribute
    {
        private const int MinimumLength = 8;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password) || password.Length < MinimumLength)
            {
                return new ValidationResult($"Password must be at least {MinimumLength} characters long.");
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult("Password must contain at least one digit.");
            }

            if (!Regex.IsMatch(password, @"[\W_]"))
            {
                return new ValidationResult("Password must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}
