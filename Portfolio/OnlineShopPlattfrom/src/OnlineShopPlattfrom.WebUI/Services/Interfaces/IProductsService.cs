using OnlineShopPlattfrom.SharedLibrary.Internal;

namespace OnlineShopPlattfrom.WebUI.Services.Interfaces
{
    public interface IProductsService
    {
        Task<ProductBase?> GetProductByIdAndCategory(Guid id, string category);
        Task<IEnumerable<ProductBase>> GetProductsByCategory(string category);
    }
}