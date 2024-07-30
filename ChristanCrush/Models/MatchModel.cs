namespace ChristanCrush.Models
{
    public class MatchModel
    {

        public int MatchId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public DateTime MatchedAt { get; set; } = DateTime.UtcNow;
    }
}
