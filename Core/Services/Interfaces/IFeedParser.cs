using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IFeedParser<T> where T: class
    {
        Task<T> ParseAsync(string url);
    }
}
