using Microsoft.EntityFrameworkCore;

using OnlineShopPlattfrom.WebAPI.Data;
using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : Product
{
    private readonly AppDbContext dbContext;
    public GenericRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync() 
        => await dbContext.Set<T>().ToListAsync();
    
    public async Task<T?> GetByIdAsync(Guid id) 
        => await dbContext.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
    }
    
    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);
        
        if(entity is null)
        {
            return false;
        }

        dbContext.Set<T>().Remove(entity);
        
        await dbContext.SaveChangesAsync();

        return true;
    }
}
