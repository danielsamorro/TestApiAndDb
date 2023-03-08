using CreditProcessor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CreditProcessor.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CreditProcessorContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(CreditProcessorContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public Task<List<T>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public ValueTask<T?> GetById(Guid id)
        {
            return _dbSet.FindAsync(id);
        }
    }
}
