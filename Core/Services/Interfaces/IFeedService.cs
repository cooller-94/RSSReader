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
        Task Create(FeedDTO feed, string category, string userId);

        Task<IEnumerable<FeedDTO>> GetAllByTerm(string term);
        Task<IEnumerable<FeedInformation>> GetAllFeedsAsync(string userId);

    }
}
