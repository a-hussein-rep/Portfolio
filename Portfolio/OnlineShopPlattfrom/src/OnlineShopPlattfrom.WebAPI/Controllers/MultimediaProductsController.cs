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
    public class MultimediaProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MultimediaProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MultimediaProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MultimediaProduct>>> GetMultimediaProducts()
        {
            return await _context.MultimediaProducts.ToListAsync();
        }

        // GET: api/MultimediaProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MultimediaProduct>> GetMultimediaProduct(Guid id)
        {
            var multimediaProduct = await _context.MultimediaProducts.FindAsync(id);

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

            _context.Entry(multimediaProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<MultimediaProduct>> PostMultimediaProduct(MultimediaProduct multimediaProduct)
        {
            _context.MultimediaProducts.Add(multimediaProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMultimediaProduct", new { id = multimediaProduct.Id }, multimediaProduct);
        }

        // DELETE: api/MultimediaProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMultimediaProduct(Guid id)
        {
            var multimediaProduct = await _context.MultimediaProducts.FindAsync(id);
            if (multimediaProduct == null)
            {
                return NotFound();
            }

            _context.MultimediaProducts.Remove(multimediaProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MultimediaProductExists(Guid id)
        {
            return _context.MultimediaProducts.Any(e => e.Id == id);
        }
    }
}
