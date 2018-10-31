using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string PictureUrl { get; set; }

        public IEnumerable<UserFeed> Feeds { get; set; }
        public IEnumerable<UserPostDetail> PostDetails { get; set; }
    }
}
