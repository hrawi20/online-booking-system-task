using System.Linq.Expressions;
using Booking.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly BookingDbContext _context;

    public Repository(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        List<Expression<Func<T, bool>>>? predicates = null,
        List<Expression<Func<T, object>>>? includes = null
    )
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes == null)
            return await query.ToListAsync();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (predicates == null)
            return await query.ToListAsync();

        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(
            entity => (int)typeof(T).GetProperty("Id").GetValue(entity) == id
        );
    }

    // public async Task<IEnumerable<T>> GetAllAsync()
    // {
    //     return await _context.Set<T>().ToListAsync();
    // }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
}
