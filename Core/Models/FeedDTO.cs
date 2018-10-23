
namespace Core.Models
{
    public class FeedDTO
    {
        public int FeedId { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
