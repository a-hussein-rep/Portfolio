using Microsoft.AspNetCore.Mvc;

using OnlineShopPlattfrom.SharedLibrary.Models;

using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository productsRepository;

    public ProductsController(IProductsRepository productsRepository)
    {
        this.productsRepository = productsRepository;
    }

    [HttpGet("list/{category}")] //api/products/list/Multiymedia
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        return Ok(await this.productsRepository.GetProductsByCategoryAsync(category));
    }

    [HttpGet("{id}")] //api/products/b2fa1c18-6baf-4e5c-9285-1f8bfaf2cd0f
    public async Task<IActionResult> GetOneById(Guid id)
    {
        var product = await this.productsRepository.GetProductByIdAsync(id);

        if (product is null)
        {
            return NotFound($"Product with ID '{id}' not found.");
        }

        return Ok(product);
    }

    [HttpPost] //api/product
    public async Task<IActionResult> PostOne([FromBody] ProductModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Product product;
        
        try
        {
            product = await this.productsRepository.AddAsync(model);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return CreatedAtAction(nameof(GetOneById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")] //api/product/b2fa1c18-6baf-4e5c-9285-1f8bfaf2cd0f
    public async Task<IActionResult> UpdateOne(Guid id, [FromBody] ProductModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Product? product; 

        try
        {
            product = await this.productsRepository.UpdateAsync(id, model);

            if(product is null)
            {
                return NotFound("Product not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")] //api/product/b2fa1c18-6baf-4e5c-9285-1f8bfaf2cd0f
    public async Task<IActionResult> DeleteProductById(Guid id)
    {
        try
        {
            await this.productsRepository.DeleteAsync(id);

            return NoContent();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
