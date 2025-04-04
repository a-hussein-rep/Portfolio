using OnlineShopPlattfrom.SharedLibrary.Internal;

namespace OnlineShopPlattfrom.SharedLibrary.Models;

public class WearableProductModel : ProductBase
{
    public string? Size { get; set; }

    public string? Color { get; set; }
    
    public string? Fabric { get; set; }
    
    public string? Pattern { get; set; }
    
    public string? SleeveType { get; set; }
    
    public string? NeckStyle { get; set; }
}
