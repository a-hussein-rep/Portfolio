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

    public async Task<ProductBase?> GetProductByIdAndCategory(Guid id, string endpoint)
        => endpoint switch
        {
            "multimedia" => await httpClient
                .GetFromJsonAsync<MultimediaProductModel>($"api/{endpoint}/{id}"),

            "wearable" => await httpClient
                .GetFromJsonAsync<WearableProductModel>($"api/{endpoint}/{id}"),

            _ => throw new NotSupportedException($"Unknown product type: {endpoint}")
        };

    public async Task<IEnumerable<ProductBase>> GetProductsByCategory(string endpoint)
        => endpoint switch
        {
            "multimedia" => await httpClient
                .GetFromJsonAsync<IEnumerable<MultimediaProductModel>>($"api/{endpoint}/") ?? [],

            "wearable" => await httpClient
                .GetFromJsonAsync<IEnumerable<WearableProductModel>>($"api/{endpoint}/") ?? [],

            _ => []

        };
}
