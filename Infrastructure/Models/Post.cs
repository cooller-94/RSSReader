using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Infrastructure.Models
{

    public class Post
    {
        public int PostId { get; set; }

        public string PostHash { get; set; }

        public DateTime DateAdded { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string CommentsUrl { get; set; }

        public string Link { get; set; }

        public DateTime? PublishDate { get; set; }

        public int FeedId { get; set; }

        public Feed Feed { get; set; }

        public IEnumerable<UserPostDetail> PostDetails { get; set; }
    }
}
