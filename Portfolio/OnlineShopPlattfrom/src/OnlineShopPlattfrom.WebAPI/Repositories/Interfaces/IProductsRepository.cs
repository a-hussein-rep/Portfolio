using OnlineShopPlattfrom.SharedLibrary.Models;
using OnlineShopPlattfrom.WebAPI.Data.Entities;

namespace OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

public interface IProductsRepository
{
    Task<ICollection<Product>> GetProductsByCategoryAsync(string category);

    Task<Product?> GetProductByIdAsync(Guid id);

    Task<Product> AddAsync(ProductModel model);

    Task<Product?> UpdateAsync(Guid id, ProductModel model);

    Task DeleteAsync(Guid id);
}