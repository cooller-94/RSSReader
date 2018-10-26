using System;

namespace Web.Models
{
    public class PostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool isRead { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Author { get; set; }
        public string CommentsUrl { get; set; }
        public FeedModel Feed { get; set; }

        public string PublishAgo
        {
            get
            {
                if (!PublishDate.HasValue)
                {
                    return null;
                }

                TimeSpan subDate = DateTime.Now - PublishDate.Value;

                if (subDate.TotalHours > 24)
                {
                    return $"{(int)subDate.TotalDays}d ago";
                }

                if (subDate.TotalHours < 1)
                {
                    return $"{(int)subDate.TotalMinutes}m ago";
                }

                return $"{(int)subDate.TotalHours}h ago";
            }
        }

    }
}
