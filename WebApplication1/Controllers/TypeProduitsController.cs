using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EntityFramework;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProduitsController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public TypeProduitsController(ProductDBContext context)
        {
            _context = context;
        }

        // GET: api/TypeProduits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetTypesProduit()
        {
          if (_context.TypesProduit == null)
          {
              return NotFound();
          }
            return await _context.TypesProduit.ToListAsync();
        }

        // GET: api/TypeProduits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduit>> GetTypeProduit(int id)
        {
          if (_context.TypesProduit == null)
          {
              return NotFound();
          }
            var typeProduit = await _context.TypesProduit.FindAsync(id);

            if (typeProduit == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        // PUT: api/TypeProduits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProduit(int id, TypeProduit typeProduit)
        {
            if (id != typeProduit.TypeProduitID)
            {
                return BadRequest();
            }

            _context.Entry(typeProduit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeProduitExists(id))
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

        // POST: api/TypeProduits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit typeProduit)
        {
          if (_context.TypesProduit == null)
          {
              return Problem("Entity set 'ProductDBContext.TypesProduit'  is null.");
          }
            _context.TypesProduit.Add(typeProduit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeProduit", new { id = typeProduit.TypeProduitID }, typeProduit);
        }

        // DELETE: api/TypeProduits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProduit(int id)
        {
            if (_context.TypesProduit == null)
            {
                return NotFound();
            }
            var typeProduit = await _context.TypesProduit.FindAsync(id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            _context.TypesProduit.Remove(typeProduit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeProduitExists(int id)
        {
            return (_context.TypesProduit?.Any(e => e.TypeProduitID == id)).GetValueOrDefault();
        }
    }
}
