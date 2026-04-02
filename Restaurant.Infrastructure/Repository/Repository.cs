using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;

namespace Restaurant.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly RestaurantContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(RestaurantContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
