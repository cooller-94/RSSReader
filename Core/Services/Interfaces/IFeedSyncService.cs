using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IFeedSyncService
    {
        Task<IEnumerable<SyncFeedResult>> SyncAllAsync(string userId);
    }
}
