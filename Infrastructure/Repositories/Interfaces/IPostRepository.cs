using Infrastructure.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetPostsByHashAsync(string[] hashs);
        Task<Post> GetPostByHashAsync(string hash);
        Task<int> GetCountPostsByFeedId(int feedId);
    }
}
