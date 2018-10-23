
namespace Core.Models
{
    public class FeedInformation
    {
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int PostsCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
