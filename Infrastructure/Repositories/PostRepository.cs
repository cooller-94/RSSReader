using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RSSReaderContext context) : base(context)
        {

        }

        public async Task<int> GetCountPostsByFeedId(int feedId)
        {
            return await _context.Posts.Where(i => i.FeedId == feedId && !i.IsRead).CountAsync();
        }

        public async Task<Post> GetPostByHashAsync(string hash)
        {
            return await _context.Posts.FirstOrDefaultAsync(i => i.PostHash == hash);
        }

        public async Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs)
        {
            return await _context.Posts.Where(item => hashs.Contains(item.PostHash)).ToListAsync();
        }
    }
}
