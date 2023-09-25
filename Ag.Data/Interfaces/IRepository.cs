using System.Linq.Expressions;

namespace Ag.BLL.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    bool Any(Expression<Func<TEntity, bool>> filter);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByCodeAsync(int code);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}
