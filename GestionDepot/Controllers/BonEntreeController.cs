using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GestionDepot.Models;
using GestionDepot.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonEntreeController : ControllerBase
    {
        private readonly GestionDBContext _context;
        private readonly ILogger<BonEntreeController> _logger;

        public BonEntreeController(GestionDBContext context, ILogger<BonEntreeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/BonEntree
        // GET: api/BonEntree
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BonEntree>>> GetBonEntrees()
        {
            var bonEntrees = await _context.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Societe)
                .ToListAsync();

            // Fix invalid date values
            foreach (var bonEntree in bonEntrees)
            {
                if (bonEntree.Date == DateTime.MinValue)
                {
                    bonEntree.Date = new DateTime(1970, 1, 1); // Set a default date if needed
                }
            }

            return bonEntrees;
        }


        // POST: api/BonEntree
        [HttpPost]
        public async Task<ActionResult<BonEntree>> CreateBonEntree(BonEntreeDto bonEntreeDto)
        {
            _logger.LogInformation("Received POST request to create BonEntree");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return BadRequest(ModelState);
            }

            // Validate foreign keys
            if (bonEntreeDto.IdFournisseur.HasValue)
            {
                var fournisseurExists = await _context.Fournisseurs.AnyAsync(f => f.Id == bonEntreeDto.IdFournisseur);
                if (!fournisseurExists)
                {
                    _logger.LogWarning($"Invalid Fournisseur Id: {bonEntreeDto.IdFournisseur}");
                    return BadRequest("Invalid Fournisseur Id");
                }
            }

            if (bonEntreeDto.IdProduit.HasValue)
            {
                var produitExists = await _context.Produits.AnyAsync(p => p.Id == bonEntreeDto.IdProduit);
                if (!produitExists)
                {
                    _logger.LogWarning($"Invalid Produit Id: {bonEntreeDto.IdProduit}");
                    return BadRequest("Invalid Produit Id");
                }
            }

            if (bonEntreeDto.IdChambre.HasValue)
            {
                var chambreExists = await _context.Chambres.AnyAsync(c => c.Id == bonEntreeDto.IdChambre);
                if (!chambreExists)
                {
                    _logger.LogWarning($"Invalid Chambre Id: {bonEntreeDto.IdChambre}");
                    return BadRequest("Invalid Chambre Id");
                }
            }

            if (bonEntreeDto.IdSociete.HasValue)
            {
                var societeExists = await _context.Societes.AnyAsync(s => s.Id == bonEntreeDto.IdSociete);
                if (!societeExists)
                {
                    _logger.LogWarning($"Invalid Societe Id: {bonEntreeDto.IdSociete}");
                    return BadRequest("Invalid Societe Id");
                }
            }

            var bonEntree = new BonEntree
            {
                Date = bonEntreeDto.Date,
                Qte = bonEntreeDto.Qte,
                IdFournisseur = bonEntreeDto.IdFournisseur,
                IdProduit = bonEntreeDto.IdProduit,
                IdChambre = bonEntreeDto.IdChambre,
                IdSociete = bonEntreeDto.IdSociete,
            };

            _context.BonEntrees.Add(bonEntree);
            await _context.SaveChangesAsync();

            // Inclure les propriétés de navigation
            var createdBonEntree = await _context.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Societe)
                .FirstOrDefaultAsync(b => b.Id == bonEntree.Id);

            _logger.LogInformation($"Created BonEntree with Id: {createdBonEntree.Id}");

            return CreatedAtAction(nameof(GetBonEntree), new { id = createdBonEntree.Id }, createdBonEntree);
        }

        // GET: api/BonEntree/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BonEntree>> GetBonEntree(int id)
        {
            var bonEntree = await _context.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Societe)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bonEntree == null)
            {
                return NotFound();
            }

            return bonEntree;
        }

        // PUT: api/BonEntree/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBonEntree(int id, BonEntreeDto bonEntreeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonEntree = await _context.BonEntrees.FindAsync(id);
            if (bonEntree == null)
            {
                return NotFound();
            }

            // Update the BonEntree entity
            bonEntree.Date = bonEntreeDto.Date;
            bonEntree.Qte = bonEntreeDto.Qte;
            bonEntree.IdFournisseur = bonEntreeDto.IdFournisseur;
            bonEntree.IdProduit = bonEntreeDto.IdProduit;
            bonEntree.IdChambre = bonEntreeDto.IdChambre;
            bonEntree.IdSociete = bonEntreeDto.IdSociete;

            _context.Entry(bonEntree).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BonEntreeExists(id))
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

        // DELETE: api/BonEntree/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBonEntree(int id)
        {
            var bonEntree = await _context.BonEntrees.FindAsync(id);
            if (bonEntree == null)
            {
                return NotFound();
            }

            _context.BonEntrees.Remove(bonEntree);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BonEntreeExists(int id)
        {
            return _context.BonEntrees.Any(e => e.Id == id);
        }
    }
}
