using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("post_hash")]
        public string PostHash { get; set; }

        [Column("date_added")]
        public DateTime DateAdded { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("comments_url")]
        public string CommentsUrl { get; set; }

        [Column("link")]
        public string Link { get; set; }

        [Column("publish_date")]
        public DateTime? PublishDate { get; set; }

        [Column("is_read")]
        public bool IsRead { get; set; }

        [ForeignKey("feed_id")]
        [Column("feed_id")]
        public int FeedId { get; set; }

        public Feed Feed { get; set; }
    }
}
