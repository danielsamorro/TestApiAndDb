namespace CreditProcessor.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        ValueTask<T?> GetById (Guid id);
        Task<List<T>> GetAll();
    }
}
