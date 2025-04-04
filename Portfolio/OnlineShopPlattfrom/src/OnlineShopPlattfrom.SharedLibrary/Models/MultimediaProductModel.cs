using OnlineShopPlattfrom.SharedLibrary.Internal;

namespace OnlineShopPlattfrom.SharedLibrary.Models;

public class MultimediaProductModel : ProductBase
{
    public string Model { get; set; }
    
    public decimal DisplaySize { get; set; }

    public string DisplayType { get; set; }

    public decimal WeightInKG { get; set; }
}
