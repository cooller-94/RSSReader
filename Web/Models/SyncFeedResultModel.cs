using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models
{
    public class SyncFeedResultModel
    {
        public int FeedId { get; set; }
        public string CategoryTitle { get; set; }
        public string FeedTitle { get; set; }
        public int PostsCount { get; set; }
    }
}
