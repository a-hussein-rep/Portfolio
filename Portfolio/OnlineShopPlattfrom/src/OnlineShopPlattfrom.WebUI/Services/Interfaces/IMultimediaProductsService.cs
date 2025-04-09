using OnlineShopPlattfrom.SharedLibrary.Models;

namespace OnlineShopPlattfrom.WebUI.Services.Interfaces;

public interface IMultimediaProductsService
{
    Task<IEnumerable<MultimediaProductModel>> GetProductsByCategory(string category);

    Task<MultimediaProductModel?> GetProductByIdAndCategory(Guid id, string category);
}
