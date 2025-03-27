namespace OnlineShopPlattfrom.WebAPI.Data.Entities;

public class Product
{
    public Guid Id { get; set; }

    public required string Name { get; set; } 

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public required string Category { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }
}