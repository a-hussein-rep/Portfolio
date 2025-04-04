using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OnlineShopPlattfrom.SharedLibrary.Models;

using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MultimediaProductsController : BaseProductController<MultimediaProduct>
{
    public MultimediaProductsController(IGenericRepository<MultimediaProduct> repository)
        : base(repository)
    { }

    // GET: api/MultimediaProducts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MultimediaProduct>>> GetMultimediaProducts()
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

    // GET: api/MultimediaProducts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MultimediaProduct>> GetMultimediaProduct(Guid id)
    {
        var multimediaProduct = await Repository.GetByIdAsync(id);

        if (multimediaProduct == null)
        {
            return NotFound();
        }

        return multimediaProduct;
    }

    // PUT: api/MultimediaProducts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMultimediaProduct(Guid id, MultimediaProduct multimediaProduct)
    {
        if (id != multimediaProduct.Id)
        {
            return BadRequest();
        }

        try
        {
            await Repository.UpdateAsync(multimediaProduct);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MultimediaProductExists(id))
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

    // POST: api/MultimediaProducts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MultimediaProduct>> PostMultimediaProduct(MultimediaProductModel model)
    {
        var multimediaProduct = new MultimediaProduct
        {
            Category = model.Category,
            Description = model.Description,
            Name = model.Name, 
            DisplaySize = model.DisplaySize, 
            DisplayType = model.DisplayType,
            ImageUrl = model.ImageUrl,
            Model = model.Model,
            Price = model.Price, 
            Quantity = model.Quantity, 
            WeightInKG = model.WeightInKG
        };

        await Repository.AddAsync(multimediaProduct);

        return CreatedAtAction("GetMultimediaProduct", new { id = multimediaProduct.Id }, multimediaProduct);
    }

    // DELETE: api/MultimediaProducts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMultimediaProduct(Guid id)
    {
        if (await Repository.DeleteAsync(id) is false)
        {
            return NotFound();
        }

        return NoContent();
    }

    private bool MultimediaProductExists(Guid id)
    {
        return Repository.GetByIdAsync(id) is not null;
    }
}
