using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChristanCrush.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }


        public string SenderName { get; set; }
        [Required]
        [DisplayName("Message")]
        public string MessageContent { get; set; }

        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
