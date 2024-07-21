using ChristanCrush.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChristanCrush.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string first_name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string last_name { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Retyped Password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirm_password { get; set; }

        [Required]
        [DisplayName("Gender")]
        [StringLength(1)]
        public string gender { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_of_birth { get; set; }

        public DateTime created_at { get; set; }

        /// <summary>
        /// The ToString method returns a string representation of an object's email and password
        /// properties.
        /// </summary>
        /// <returns>
        /// The `ToString` method is returning a string that concatenates the email and password values
        /// with the labels "Email = " and " Password = ".
        /// </returns>
        public override string ToString()
        {
            return "Email = " + email + " Password = " + password;
        }

        /// <summary>
        /// The function `IsOver18YearsOld` in C# determines if a person is over 18 years old based on
        /// their date of birth.
        /// </summary>
        /// <returns>
        /// The method `IsOver18YearsOld` returns a boolean value indicating whether the person is over
        /// 18 years old or not.
        /// </returns>
        public bool IsOver18YearsOld()
        {
            
            var today = DateTime.Today;
            var age = today.Year - date_of_birth.Year;

            
            if (date_of_birth.Date > today.AddYears(-age)) age--;

            
            return age >= 18;
        }
    }
}
