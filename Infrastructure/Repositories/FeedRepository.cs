using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FeedRepository : RepositoryBase<Feed>, IFeedRepository
    {
        public FeedRepository(RSSReaderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feed>> GetAllByTerm(string term)
        {
            var query = from feed in _context.Feeds
                        where feed.Link == term || feed.Url.Contains(term) || feed.Title.Contains(term) || feed.Description.Contains(term)
                        select feed;


            query = query.Include(i => i.Image);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Feed>> GetAllByUrl(string url)
        {
            var query = from feed in _context.Feeds
                        where feed.Url == url
                        select feed;


            query = query.Include(i => i.Image);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Feed>> GetAllFeedsForUserAsync(string userId)
        {
            return await _context.Feeds.ToListAsync();
        }
    }
}
