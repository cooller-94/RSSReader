using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("Feed")]
    public class Feed
    {
        [Key]
        [Column("feed_id")]

        public int FeedId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("url")]
        public string Url { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Column("category_id")]
        [ForeignKey("category_id")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [Column("image_id")]
        [ForeignKey("image_id")]
        public Guid? ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
