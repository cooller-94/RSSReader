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

        public async Task<int> GetCountPostsByFeedIdAsync(int feedId)
        {
            return await _context.Posts.Where(i => i.FeedId == feedId && !i.IsRead).CountAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(i => i.PostId == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs)
        {
            return await _context.Posts.Where(item => hashs.Contains(item.PostHash)).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetUnreadPostsByFeedIdAsync(int feedId)
        {
            return await _context.Posts
                .Where(i => i.FeedId == feedId && !i.IsRead)
                .Include(i => i.Feed)
                .OrderByDescending(i => i.PublishDate)
                .ThenBy(i => i.DateAdded)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetUnreadPostsByCategoryAsync(string category)
        {
            return await _context.Posts
                .Where(i => !i.IsRead && i.Feed.Category != null && i.Feed.Category.Name == category)
                .Include(i => i.Feed)
                .OrderByDescending(i => i.PublishDate)
                .ThenBy(i => i.DateAdded)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllUnreadPostsAsync()
        {
            return await _context.Posts
                .Where(i => !i.IsRead)
                .Include(i => i.Feed)
                .OrderByDescending(i => i.PublishDate)
                .ThenBy(i => i.DateAdded)
                .ToListAsync();
        }
    }
}
