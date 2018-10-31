using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserPostDetailRepository : RepositoryBase<UserPostDetail>, IUserPostDetailRepository
    {
        public UserPostDetailRepository(RSSReaderContext context) : base(context)
        {
        }
    }
}
