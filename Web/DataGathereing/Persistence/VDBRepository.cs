
using DataGathering.Api.Entities;

namespace DataGathering.Api.Persistence
{
    public class VDBRepository<T> : IRepository<T> where T : BaseEntity
    {
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Read()
        {
            throw new NotImplementedException();
        }

        public Task<T> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Update(T student)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
