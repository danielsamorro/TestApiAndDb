using CreditProcessor.Domain.Repositories;
using CreditProcessor.Domain.UnitOfWork;
using CreditProcessor.Infrastructure.Repositories;

namespace CreditProcessor.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CreditProcessorContext _context;

        public ICreditRepository CreditRepository { get; private set; }

        public UnitOfWork(CreditProcessorContext context)
        {
            _context = context;

            CreditRepository = new CreditRepository(_context);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
