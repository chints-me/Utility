using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Utility.WebApp.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly UtilityDbContext dbContext;

        public GenericRepository(UtilityDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await dbContext.AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression = null)
        {
            var entity = await Get(expression);
            return entity != null;
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null)
        {
            var result = dbContext.Set<T>().AsQueryable();
            if (expression != null) result = result.Where(expression);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            var result = dbContext.Set<T>().AsQueryable();
            if (expression != null) result = result.Where(expression);
            return await result.ToListAsync();
        }
    }
}
