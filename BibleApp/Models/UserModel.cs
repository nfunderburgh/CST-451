using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Milestone.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("State Abbreviation")]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public override string ToString()
        {
            return "Email = " + Email + " Password = " + Password;
        }
    }
}
