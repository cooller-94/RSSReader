﻿using System.Collections.Generic;
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

        public async Task<IEnumerable<Feed>> GetAllFeedsAsync()
        {
            return await _context.Feeds.Include(i => i.Category).Include(i => i.Image).ToListAsync();
        }
    }
}
