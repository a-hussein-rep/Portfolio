using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopPlattfrom.WebAPI.Data;
using OnlineShopPlattfrom.WebAPI.Data.Entities;

namespace OnlineShopPlattfrom.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WearableProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WearableProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WearableProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WearableProduct>>> GetWearableProducts()
        {
            return await _context.WearableProducts.ToListAsync();
        }

        // GET: api/WearableProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WearableProduct>> GetWearableProduct(Guid id)
        {
            var wearableProduct = await _context.WearableProducts.FindAsync(id);

            if (wearableProduct == null)
            {
                return NotFound();
            }

            return wearableProduct;
        }

        // PUT: api/WearableProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWearableProduct(Guid id, WearableProduct wearableProduct)
        {
            if (id != wearableProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(wearableProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WearableProductExists(id))
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

        // POST: api/WearableProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WearableProduct>> PostWearableProduct(WearableProduct wearableProduct)
        {
            _context.WearableProducts.Add(wearableProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWearableProduct", new { id = wearableProduct.Id }, wearableProduct);
        }

        // DELETE: api/WearableProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWearableProduct(Guid id)
        {
            var wearableProduct = await _context.WearableProducts.FindAsync(id);
            if (wearableProduct == null)
            {
                return NotFound();
            }

            _context.WearableProducts.Remove(wearableProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WearableProductExists(Guid id)
        {
            return _context.WearableProducts.Any(e => e.Id == id);
        }
    }
}
