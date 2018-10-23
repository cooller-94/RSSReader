using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPostService
    {
        Task MarkAsRead(string hash);
    }
}
