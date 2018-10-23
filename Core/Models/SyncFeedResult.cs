namespace Core.Models
{
    public class SyncFeedResult
    {
        public int FeedId { get; set; }
        public string CategoryTitle { get; set; }
        public string FeedTitle { get; set; }
        public int PostsCount { get; set; }

        public SyncFeedResult(int feedId, string category, string feedTitle)
        {
            CategoryTitle = category;
            FeedTitle = feedTitle;
            FeedId = feedId;
        }
    }
}
