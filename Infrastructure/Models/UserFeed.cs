using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class UserFeed
    {
        public string UserId { get; set; }

        public int FeedId { get; set; }

        public int CategoryId { get; set; }

        public Feed Feed { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }

        public IEnumerable<UserPostDetail> PostDetails { get; set; }
    }
}
