using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs);
        Task<Post> GetPostByIdAsync(int id);
        Task<int> GetCountPostsByFeedIdAsync(int feedId);
        Task<IEnumerable<Post>> GetUnreadPostsByFeedIdAsync(int feedId);
        Task<IEnumerable<Post>> GetUnreadPostsByCategoryAsync(string category);
        Task<IEnumerable<Post>> GetAllUnreadPostsAsync();
    }
}
