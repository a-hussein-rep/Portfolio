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

    public async Task<ProductModel?> GetProductById(Guid id)
    {
        return await httpClient.GetFromJsonAsync<ProductModel>($"api/products/{id}");
    }

    public async Task<IEnumerable<ProductModel>> GetProductsByCategory(string category)
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductModel>>($"api/products/list/{category}") 
            ?? new List<ProductModel>();

        return products;
    }
}
