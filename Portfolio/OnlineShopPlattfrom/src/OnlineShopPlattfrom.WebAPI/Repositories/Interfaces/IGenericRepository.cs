using OnlineShopPlattfrom.WebAPI.Data.Entities;

namespace OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

public interface IGenericRepository<T> where T : Product
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task<bool> DeleteAsync(Guid id);
}
