using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EntityFramework;
using WebApplication1.Models.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepository<Produit> dataRepository;

        public ProduitsController(IDataRepository<Produit> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return dataRepository.GetAll();
        }

        // GET: api/Produits/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            var produit = dataRepository.GetById(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            return produit;
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.ProduitID)
            {
                return BadRequest();
            }
            var productToUpdate = dataRepository.GetById(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                dataRepository.Update(productToUpdate.Value, produit);
                return NoContent();
            }
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dataRepository.Add(produit);
            return CreatedAtAction("GetById", new { id = produit.ProduitID }, produit); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = dataRepository.GetById(id);
            if (produit == null)
            {
                return NotFound();
            }
            dataRepository.Delete(produit.Value);
            return NoContent();
        }

        /*
        private bool ProduitExists(int id)
        {
            return (dataRepository.Produits?.Any(e => e.ProduitID == id)).GetValueOrDefault();
        }
        */
    }
}
