using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPostService
    {
        Task MarkAsRead(int id);
        Task<IEnumerable<PostDTO>> GetUnreadPostsAsync(int feedId, string userId);
        Task<IEnumerable<PostDTO>> GetUnreadPostsByCategoryAsync(string category, string userId);
        Task<IEnumerable<PostDTO>> GetAllUnreadPostsAsync(string userId);
    }
}
