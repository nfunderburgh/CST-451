namespace ChristanCrush.Models
{
    public class LikeModel
    {
        public int LikeId { get; set; }
        public int LikerId { get; set; }
        public int LikedId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;

    }
}
