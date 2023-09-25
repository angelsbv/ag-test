using Ag.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ag.DAL.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public bool Any(Expression<Func<TEntity, bool>> filter)
    {
        return Context.Set<TEntity>().Any(filter);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByCodeAsync(int code)
    {
        return await Context.Set<TEntity>().FindAsync(code);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int code)
    {
        var entity = await GetByCodeAsync(code);
        if (entity == null)
            return false;

        Context.Set<TEntity>().Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }
}
