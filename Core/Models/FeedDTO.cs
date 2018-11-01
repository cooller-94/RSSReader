
namespace Core.Models
{
    public class FeedDTO
    {
        public int FeedId { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }
        public string Link { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public ImageDTO Image { get; set; }
    }
}
