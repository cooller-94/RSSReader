using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("FeedUser")]
    public class FeedUser
    {
        public int FeedUserId { get; set; }

        public string UserId { get; set; }

        public int FeedId { get; set; }

        public int CategoryId { get; set; }

        public Feed Feed { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
    }
}
