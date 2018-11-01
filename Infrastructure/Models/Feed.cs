using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Feed
    {
        public int FeedId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Link { get; set; }

        public Guid? ImageId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<UserFeed> Users { get; set; }
    }
}
