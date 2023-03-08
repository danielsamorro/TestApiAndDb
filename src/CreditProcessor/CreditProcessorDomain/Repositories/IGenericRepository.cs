namespace CreditProcessor.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
    }
}
