using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Finance.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Infrastructure.DataAcess.Repositories; 
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity 
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext context) {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task AddOneAsync(TEntity entity) {
        
        await _dbSet.AddAsync(entity);

        await _context.SaveChangesAsync();
       
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> filterExpression) {

        var entities = await _dbSet.Where(filterExpression).ToListAsync();

        if (entities == null || entities.Count == 0) return;

        foreach (var entity in entities) {
            if (entity.Status != BaseStatus.deleted) {
                entity.Status = BaseStatus.deleted;
                _dbSet.Update(entity);
            }
        }
    }

    public async Task DeleteAsync(Guid id) {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null && entity.Status != BaseStatus.deleted) 
        {

            entity.Status = BaseStatus.deleted;

            _dbSet.Update(entity);

        }
    }

    public async Task ReplaceOneAsync(Expression<Func<TEntity, bool>> filterExpression, TEntity entity) {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(filterExpression);
        if (existingEntity != null) {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filterExpression);
    }

    public async Task<TEntity> GetOneIncludingInactiveAsync(Expression<Func<TEntity, bool>> filterExpression) {
        return await _dbSet.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(filterExpression);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 10) {
        return await _dbSet.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllIncludingInactiveAsync() {
        return await _dbSet.IgnoreQueryFilters().ToListAsync();
    }

    public async Task<int> CountAsync() {
        return await _dbSet.CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression) {
        return await _dbSet.CountAsync(filterExpression);
    }

    public IQueryable<TEntity> GetQueryable() {
        return _dbSet.AsQueryable();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression) {
        return await _dbSet.AnyAsync(filterExpression);
    }
}
