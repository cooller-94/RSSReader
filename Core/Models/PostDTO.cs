using System;

namespace Core.Models
{
    public class PostDTO
    {
        public int PostId { get; set; }

        public DateTime DateAdded { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string CommentsUrl { get; set; }

        public string Link { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsRead { get; set; }

        public FeedDTO Feed { get; set; }
        public string PostHash { get; set; }
    }
}
