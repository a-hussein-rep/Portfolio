using OnlineShopPlattfrom.SharedLibrary.Internal;
using OnlineShopPlattfrom.SharedLibrary.Models;
using OnlineShopPlattfrom.WebUI.Services.Interfaces;

namespace OnlineShopPlattfrom.WebUI.Services;

public class ProductsService : IProductsService
{
    private readonly HttpClient httpClient;

    public ProductsService(IHttpClientFactory clientFactory)
    {
        httpClient = clientFactory.CreateClient("ProductClient");
    }

    public async Task<ProductBase?> GetProductByIdAndCategory(Guid id, string category)
        => category switch
        {
            "MultimediaProducts" => await httpClient
                .GetFromJsonAsync<MultimediaProductModel>($"api/{category}/{id}"),

            _ => throw new NotSupportedException($"Unknown product type: {category}")
        };

    public async Task<IEnumerable<ProductBase>> GetProductsByCategory(string category)
        => category switch
        {
            "MultimediaProducts" => await httpClient
                .GetFromJsonAsync<IEnumerable<MultimediaProductModel>>($"api/{category}/") ?? [],

            _ => []

        };
}
