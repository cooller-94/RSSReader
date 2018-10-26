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

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public Guid? ImageId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<FeedUser> Users { get; set; }
    }
}
