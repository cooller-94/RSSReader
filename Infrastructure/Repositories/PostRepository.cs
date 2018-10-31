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

        public async Task<int> GetCountUnreadPostsByFeedIdAsync(int feedId, string userId)
        {
            var query = from post in _context.Posts
                        where post.FeedId == feedId
                        join postDetail in _context.UserPostDetails on new { PostId = post.PostId, UserId = userId } equals new { PostId = postDetail.PostId, UserId = postDetail.UserId } into gj
                        from subdetail in gj.DefaultIfEmpty()
                        where subdetail == null || !subdetail.IsRead
                        select post;

            return await query.CountAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(i => i.PostId == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs)
        {
            return await _context.Posts.Where(item => hashs.Contains(item.PostHash)).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetUnreadPostsByFeedIdAsync(int feedId, string userId, int pageIndex, int totalCount)
        {
            var query = from post in _context.Posts
                        where post.FeedId == feedId
                        join postDetail in _context.UserPostDetails on new { PostId = post.PostId, UserId = userId } equals new { PostId = postDetail.PostId, UserId = postDetail.UserId } into gj
                        from subdetail in gj.DefaultIfEmpty()
                        where subdetail == null || !subdetail.IsRead
                        orderby post.PublishDate descending, post.DateAdded
                        select post;

            query = query.Include(i => i.Feed);

            return await query.Skip((pageIndex - 1) * totalCount).Take(totalCount).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetUnreadPostsByCategoryAsync(int categoryId, string userId, int pageIndex, int totalCount)
        {
            var query = from post in _context.Posts
                        join userFeed in _context.UserFeeds on new { post.FeedId, UserId = userId } equals new { userFeed.FeedId, userFeed.UserId } into userFeeds
                        from uf in userFeeds
                        where uf.CategoryId == categoryId
                        join postDetail in _context.UserPostDetails on new { PostId = post.PostId, UserId = userId } equals new { PostId = postDetail.PostId, UserId = postDetail.UserId } into gj
                        from subdetail in gj.DefaultIfEmpty()
                        where subdetail == null || !subdetail.IsRead
                        orderby post.PublishDate descending, post.DateAdded
                        select post;

            query = query.Include(i => i.Feed);

            return await query.Skip((pageIndex - 1) * totalCount).Take(totalCount).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllUnreadPostsAsync(string userId)
        {
            var query = from post in _context.Posts
                        join userFeed in _context.UserFeeds on new { post.FeedId, UserId = userId } equals new { userFeed.FeedId, userFeed.UserId } into userFeeds
                        from uf in userFeeds
                        join postDetail in _context.UserPostDetails on new { PostId = post.PostId, UserId = userId } equals new { PostId = postDetail.PostId, UserId = postDetail.UserId } into gj
                        from subdetail in gj.DefaultIfEmpty()
                        where subdetail == null || !subdetail.IsRead
                        orderby post.PublishDate descending, post.DateAdded
                        select post;

            query = query.Include(i => i.Feed);

            return await query.ToListAsync();
        }
    }
}
