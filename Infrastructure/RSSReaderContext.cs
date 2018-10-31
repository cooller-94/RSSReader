using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RSSReaderContext : IdentityDbContext<User>
    {
        public RSSReaderContext(DbContextOptions<RSSReaderContext> options)
           : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<UserFeed> UserFeeds { get; set; }
        public DbSet<UserPostDetail> UserPostDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFeed>().HasKey(x => new { x.FeedId, x.UserId });
            modelBuilder.Entity<UserPostDetail>().HasKey(x => new { x.PostId, x.UserId });
        }
    }
}
