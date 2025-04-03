using OnlineShopPlattfrom.SharedLibrary.Models;

namespace OnlineShopPlattfrom.WebUI.Services.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductModel>> GetProductsByCategory(string category);

    Task<ProductModel?> GetProductById(Guid id);
}
