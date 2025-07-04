using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain.Repositories;
public interface IBaseRepository<TEntity> where TEntity : class {
    Task AddOneAsync(TEntity entity);
    Task AddManyAsync(List<TEntity> entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task ReplaceOneAsync(Expression<Func<TEntity, bool>> filterExpression, TEntity entity);
    Task ReplaceManyAsync(List<TEntity> entities, Func<TEntity, object> keySelector);
    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetOneIncludingInactiveAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression);
    IQueryable<TEntity> GetQueryable();
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
