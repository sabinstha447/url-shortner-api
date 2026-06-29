using Microsoft.EntityFrameworkCore;
using URl_Shortner.Models;

namespace URl_Shortner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<UrlVisit> UrlVisits { get; set; }

    }
}
