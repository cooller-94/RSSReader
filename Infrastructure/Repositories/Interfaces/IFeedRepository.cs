using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IFeedRepository : IRepositoryBase<Feed>
    {
        Task<IEnumerable<Feed>> GetAllFeedsForUserAsync(string userId);
    }
}
