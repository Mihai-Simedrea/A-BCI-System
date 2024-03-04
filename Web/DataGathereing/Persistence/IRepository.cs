using DataGathering.Api.Entities;

namespace DataGathering.Api.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Read();
        Task<T> AddAsync(T entity);
        Task<T> TryGetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
