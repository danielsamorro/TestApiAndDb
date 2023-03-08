using CreditProcessor.Domain.Repositories;

namespace CreditProcessor.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICreditRepository CreditRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
