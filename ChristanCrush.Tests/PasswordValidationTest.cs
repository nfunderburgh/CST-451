using ChristanCrush.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class PasswordValidationTest
    {
        private readonly PasswordValidation validation = new PasswordValidation();

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("short", false)]
        [InlineData("nouppercase1!", false)]
        [InlineData("NoDigit!", false)]
        [InlineData("NoSpecialChar1", false)]
        [InlineData("ValidPassword1!", true)]
        public void IsValid_Password(string password, bool expectedIsValid)
        {
            var result = validation.GetValidationResult(password, new ValidationContext(new { }));

            if (expectedIsValid)
            {
                Assert.Equal(ValidationResult.Success, result);
            }
            else
            {
                Assert.NotEqual(ValidationResult.Success, result);
            }
        }
    }
}
