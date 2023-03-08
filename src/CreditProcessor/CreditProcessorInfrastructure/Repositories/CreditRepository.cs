using CreditProcessor.Domain.Entities;
using CreditProcessor.Domain.Repositories;

namespace CreditProcessor.Infrastructure.Repositories
{
    public class CreditRepository : GenericRepository<Credit>, ICreditRepository
    {
        public CreditRepository(CreditProcessorContext context) : base(context)
        {
        }
    }
}
