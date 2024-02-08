using System.Linq.Expressions;

namespace Booking.Infrastructure.Repositories;

public interface IRepository<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync(
        List<Expression<Func<T, bool>>>? predicates = null,
        List<Expression<Func<T, object>>>? includes = null
    );
    Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includeProperties);

    // Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
