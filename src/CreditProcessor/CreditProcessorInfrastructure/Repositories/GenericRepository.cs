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
            _dbSet.Add(entity);
        }
    }
}
