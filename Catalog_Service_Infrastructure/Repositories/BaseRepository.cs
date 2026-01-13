using Catalog_Service_Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog_Service_Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbSet<T> Entity { get; set; }
        public BaseRepository(AppDbContext context)
        {
            Entity = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entity.AsNoTracking().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await Entity.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await Entity.AnyAsync(predicate);
        }
  
        public IEnumerable<T> FindIncludes(Expression<Func<T, bool>> match, string[] includes)
        {
            IQueryable<T> query = Entity;

            foreach (var include in includes)
                query = query.Include(include);

            return query.Where(match).ToList();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await Entity.CountAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await Entity.FindAsync(id);
        }

        public Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
