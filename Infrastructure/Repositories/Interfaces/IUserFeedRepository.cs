using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserFeedRepository : IRepositoryBase<FeedUser>
    {
        Task<IEnumerable<FeedUser>> GetAllFeedUsersAsync(string userId);
    }
}
