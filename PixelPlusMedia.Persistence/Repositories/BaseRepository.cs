using Microsoft.EntityFrameworkCore;
using PixelPlusMedia.Application.Contracts.Persistence;

namespace PixelPlusMedia.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<T> GetByIdAsync(Guid id)
    {
        var result = await _dbContext.Set<T>().FindAsync(id);
        return result;
    }
    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<T> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
