using Core.Models;
using Core.Models.RSS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedDTO>> GetAllByCategoryAsync(string category);
        Task<IEnumerable<FeedDTO>> GetAllAsync();
        Task Create(FeedDTO feed);

        Task<RSSFeed> FindFeed(string url);
        Task<IEnumerable<FeedInformation>> GetAllFeedsAsync();
    }
}
