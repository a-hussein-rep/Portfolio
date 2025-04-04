using OnlineShopPlattfrom.SharedLibrary.Models;
using OnlineShopPlattfrom.WebUI.Services.Interfaces;

namespace OnlineShopPlattfrom.WebUI.Services.Implementations;

public class ProductsService : IProductsService
{
    private readonly HttpClient httpClient;

    public ProductsService(IHttpClientFactory clientFactory)
    {
        this.httpClient = clientFactory.CreateClient("ProductClient");    
    }

    public async Task<MultimediaProductModel?> GetProductByIdAndCategory(Guid id, string category)
    {
        return await httpClient.GetFromJsonAsync<MultimediaProductModel>($"api/{category}/{id}");
    }

    public async Task<IEnumerable<MultimediaProductModel>> GetProductsByCategory(string category)
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<MultimediaProductModel>>($"api/{category}/") 
            ?? new List<MultimediaProductModel>();

        return products;
    }
}
