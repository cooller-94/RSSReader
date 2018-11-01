using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ISearchFeedManager
    {
        Task<IEnumerable<FeedDTO>> SearchFeeds(string term);
    }
}
