using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs);
        Task<Post> GetPostByIdAsync(int id);
        Task<int> GetCountUnreadPostsByFeedIdAsync(int feedId, string userId);
        Task<IEnumerable<Post>> GetUnreadPostsByFeedIdAsync(int feedId, string userId, int pageIndex, int totalCount);
        Task<IEnumerable<Post>> GetUnreadPostsByCategoryAsync(int categoryId, string userId, int pageIndex, int totalCount);
        Task<IEnumerable<Post>> GetAllUnreadPostsAsync(string userId);
    }
}
