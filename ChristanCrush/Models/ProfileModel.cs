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

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        [DisplayName("Profile Picture")]
        public IFormFile Image1 { get; set; }

        public byte[] Image1Data { get; set; }


        [DisplayName("Additional Image 1")]
        public IFormFile Image2 { get; set; }

        public byte[] Image2Data { get; set; }

        [DisplayName("Additional Image 2")]
        public IFormFile Image3 { get; set; }

        public byte[] Image3Data { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        public string Hobbies { get; set; }
    }
}