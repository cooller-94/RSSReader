using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RSSReaderContext context): 
            base(context)
        {

        }

        public async Task<Category> GetCategoryByTitle(string title)
        {
            return await _context.Categories.Where(i => i.Name.Trim() == title).FirstOrDefaultAsync();
        }
    }
}
