namespace Infrastructure.Models
{
    public class UserPostDetail
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool IsRead { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
