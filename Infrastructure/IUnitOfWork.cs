using Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IFeedRepository FeedRepository { get; }
        IPostRepository PostRepository { get; }
        Task SaveAsync();
    }
}
