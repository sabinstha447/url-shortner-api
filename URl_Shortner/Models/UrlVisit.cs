namespace URl_Shortner.Models
{
    public class UrlVisit
    {
        public int Id { get; set; }
        public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? Referrer { get; set; }
        public string? UserAgent { get; set; }

        public int ShortUrlId { get; set; }
        public ShortUrl ShortUrl { get; set; } = null!;
    }
}
