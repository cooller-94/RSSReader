using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class FeedModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }
        public string CategoryTitle { get; set; }
    }
}
