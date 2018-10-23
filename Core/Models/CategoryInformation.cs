using System.Collections.Generic;

namespace Core.Models
{
    public class CategoryInformation
    {
        public string CategoryTitle { get; set; }

        public IEnumerable<FeedInformation> Feeds { get; set; }
    }
}
