using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task Create(CategoryDTO category);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesForUser(string userId);
    }
}
