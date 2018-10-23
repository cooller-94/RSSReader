using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        protected RSSReaderContext _context;

        public RepositoryBase(RSSReaderContext context) => _context = context;

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
