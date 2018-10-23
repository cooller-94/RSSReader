using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RSSReaderContext : DbContext
    {
        public RSSReaderContext(DbContextOptions<RSSReaderContext> options)
           : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Post> Posts { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Post>().HasIndex().IsUnique();
        //    builder.Entity<Category>().HasIndex().IsUnique();
        //}
    }
}
