using Microsoft.EntityFrameworkCore;

using OnlineShopPlattfrom.SharedLibrary.Models;

using OnlineShopPlattfrom.WebAPI.Data;
using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Repositories.Implementations;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext dbContext;

    public ProductsRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<Product>> GetProductsByCategoryAsync(string category)
    {
        var products = await dbContext.Products
            .Where(p => p.Category == category)
            .ToListAsync();

        if (products is null || products.Count == 0)
        {
            return new List<Product>();
        }

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> AddAsync(ProductModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Category = model.Category,
            Price = model.Price,
            Quantity = model.Quantity,
            ImageUrl = model.ImageUrl ?? string.Empty
        };

        await dbContext.Products.AddAsync(product);

        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, ProductModel model)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return null;
        }

        product.Name = model.Name;
        product.Description = model.Description;
        product.Category = model.Category;
        product.Price = model.Price;
        product.Quantity = model.Quantity;
        product.ImageUrl = model.ImageUrl ?? string.Empty;

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await dbContext.Products.FindAsync(id);

        if (product is not null)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }

}
