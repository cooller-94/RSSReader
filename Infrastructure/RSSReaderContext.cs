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

        public DbSet<FeedUser> UserFeeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //builder.Entity<Post>().HasIndex().IsUnique();
            //builder.Entity<Category>().HasIndex().IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FeedUser>().HasKey(x => new { x.FeedId, x.UserId });

            //If you name your foreign keys correctly, then you don't need this.
            modelBuilder.Entity<FeedUser>()
                .HasOne(pt => pt.Feed)
                .WithMany(p => p.Users)
                .HasForeignKey(pt => pt.FeedId);

            modelBuilder.Entity<FeedUser>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.Feeds)
                .HasForeignKey(pt => pt.UserId);

        }
    }
}
