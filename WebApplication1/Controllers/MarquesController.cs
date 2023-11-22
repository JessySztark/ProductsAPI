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
    public class MarquesController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public MarquesController(ProductDBContext context)
        {
            _context = context;
        }

        // GET: api/Marques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
          if (_context.Marques == null)
          {
              return NotFound();
          }
            return await _context.Marques.ToListAsync();
        }

        // GET: api/Marques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
          if (_context.Marques == null)
          {
              return NotFound();
          }
            var marque = await _context.Marques.FindAsync(id);

            if (marque == null)
            {
                return NotFound();
            }

            return marque;
        }

        // PUT: api/Marques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarque(int id, Marque marque)
        {
            if (id != marque.MarqueID)
            {
                return BadRequest();
            }

            _context.Entry(marque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarqueExists(id))
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

        // POST: api/Marques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marque>> PostMarque(Marque marque)
        {
          if (_context.Marques == null)
          {
              return Problem("Entity set 'ProductDBContext.Marques'  is null.");
          }
            _context.Marques.Add(marque);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarque", new { id = marque.MarqueID }, marque);
        }

        // DELETE: api/Marques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            if (_context.Marques == null)
            {
                return NotFound();
            }
            var marque = await _context.Marques.FindAsync(id);
            if (marque == null)
            {
                return NotFound();
            }

            _context.Marques.Remove(marque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarqueExists(int id)
        {
            return (_context.Marques?.Any(e => e.MarqueID == id)).GetValueOrDefault();
        }
    }
}
