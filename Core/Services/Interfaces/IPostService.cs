using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPostService
    {
        Task MarkAsRead(int id);
        Task<IEnumerable<PostDTO>> GetUnreadPostsAsync(int feedId);
        Task<IEnumerable<PostDTO>> GetUnreadPostsByCategoryAsync(string category);
        Task<IEnumerable<PostDTO>> GetAllUnreadPostsAsync();
    }
}
