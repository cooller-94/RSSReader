using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserFeedRepository : RepositoryBase<UserFeed>, IUserFeedRepository
    {
        public UserFeedRepository(RSSReaderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserFeed>> GetAllFeedUsersAsync(string userId)
        {
            return await _context.UserFeeds
                .Where(i => i.UserId == userId)
                .Include(i => i.Feed)
                    .ThenInclude(i => i.Image)
                .Include(i => i.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
