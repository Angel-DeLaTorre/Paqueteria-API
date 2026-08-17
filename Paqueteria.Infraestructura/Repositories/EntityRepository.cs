using Microsoft.EntityFrameworkCore;
using Paqueteria.Infrastructure.Data;
using System.Linq.Expressions;
using Paqueteria.Core.Interfaces.Repositories;

namespace Paqueteria.Infrastructure.Repositories;

public class EntityRepository<T>(AppDbContext context) : IEntityRepository<T>
    where T : class
{
    public async Task<T?> GetByIdAsync(Guid id) => await context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        => await context.Set<T>().Where(predicate).ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity) => context.Set<T>().Remove(entity);
}