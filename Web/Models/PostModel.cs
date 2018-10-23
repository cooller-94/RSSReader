using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models
{
    public class PostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public FeedModel Feed { get; set; }
    }
}
