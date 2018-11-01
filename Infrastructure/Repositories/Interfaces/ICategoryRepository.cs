using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<Category> GetCategoryByTitle(string title);
        Task<IEnumerable<Category>> GetAllCategoriesForUser(string userId);
    }
}
