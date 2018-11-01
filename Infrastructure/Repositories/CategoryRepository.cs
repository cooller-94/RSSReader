using System.Collections.Generic;
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

        public async Task<IEnumerable<Category>> GetAllCategoriesForUser(string userId)
        {
            var query = from category in _context.Categories
                        join userFeed in _context.UserFeeds on category.CategoryId equals userFeed.CategoryId into uf
                        from userFeedCategory in uf
                        where userFeedCategory.UserId == userId
                        select category;

            return await query.ToListAsync();
        }

        public async Task<Category> GetCategoryByTitle(string title)
        {
            return await _context.Categories.Where(i => i.Name.Trim() == title).FirstOrDefaultAsync();
        }
    }
}
