namespace OnlineShopPlattfrom.SharedLibrary.Internal;

public abstract class ProductBase
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public required string Category { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }

    public string Manufacturer { get; set; }
}
