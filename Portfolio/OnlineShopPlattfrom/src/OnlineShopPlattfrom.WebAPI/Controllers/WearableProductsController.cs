using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OnlineShopPlattfrom.SharedLibrary.Models;

using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Controllers;

[Route("api/wearable")]
[ApiController]
public class WearableProductsController : BaseProductController<WearableProduct>
{
    public WearableProductsController(IGenericRepository<WearableProduct> repository)
        : base(repository)
    { }

    // GET: api/wearable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WearableProduct>>> GetWearableProducts()
    {
        try
        {
            var products = await Repository.GetAllAsync();

            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/wearable/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WearableProduct>> GetWearableProduct(Guid id)
    {
        var wearableProduct = await Repository.GetByIdAsync(id);

        if (wearableProduct == null)
        {
            return NotFound();
        }

        return wearableProduct;
    }

    // PUT: api/wearable/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWearableProduct(Guid id, WearableProduct wearableProduct)
    {
        // (TODO)::this need to be changed in all other controllers, because when updating a product
        // a model will be passed not a n entity class.
        if (id != wearableProduct.Id)
        {
            return BadRequest();
        }

        try
        {
            await Repository.UpdateAsync(wearableProduct);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (ProductExists(id) is false)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/wearable
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WearableProduct>> PostWearableProduct(WearableProductModel model)
    {
        var wearableProduct = new WearableProduct
        {
            Category = model.Category,
            Description = model.Description,
            Name = model.Name, 
            ImageUrl = model.ImageUrl,
            Price = model.Price, 
            Quantity = model.Quantity, 
            Color = model.Color,
            Fabric = model.Fabric,
            NeckStyle = model.NeckStyle, 
            Pattern = model.Pattern, 
            Size = model.Size, 
            SleeveType = model.SleeveType
        };

        try
        {
        await Repository.AddAsync(wearableProduct);
        }
        catch(Exception ex)
        {
            throw new NotSupportedException(ex.Message);
        }

        return CreatedAtAction("GetWearableProduct", new { id = wearableProduct.Id }, wearableProduct);
    }

    // DELETE: api/wearable/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWearableProduct(Guid id)
    {
        if (await Repository.DeleteAsync(id) is false)
        {
            return NotFound();
        }

        return NoContent();
    }
}
