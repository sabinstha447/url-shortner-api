namespace URl_Shortner.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string LongUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<UrlVisit> Visits { get; set; } = new List<UrlVisit>();
    }
}
