using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChristanCrush.Models
{
    public class ProfileModel
    {
        [Key]
        public int ProfileId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        [DisplayName("Profile Picture")]
        public byte[] Image1 { get; set; }

        [DisplayName("Additional Image 1")]
        public byte[] Image2 { get; set; }

        [DisplayName("Additional Image 2")]
        public byte[] Image3 { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        public string Hobbies { get; set; }
    }
}