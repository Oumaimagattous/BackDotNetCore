using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalCasierController : ControllerBase
    {
        private readonly GestionDBContext _dbContext;

        public JournalCasierController(GestionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(int societeId)
        {
            var allEntries = _dbContext.JournalCasiers
                .Include(j => j.BonEntree)
                .Include(j => j.BonSortie)
                .Include(j => j.Produit)
                .Include(j => j.Societe)
                .Where(j => j.IdSociete == societeId)
                .OrderBy(j => j.Date)
                .ToList();

            var stockByProduct = new Dictionary<int, int>();
            var result = new List<JournalCasierDto>();

            foreach (var entry in allEntries)
            {
                int productId = entry.IdProduit ?? 0;

                if (!stockByProduct.ContainsKey(productId))
                {
                    stockByProduct[productId] = 0;
                }

                stockByProduct[productId] += entry.NbrE - entry.NbrS;

                result.Add(new JournalCasierDto
                {
                    IdBonEntree = entry.IdBonEntree,
                    IdBonSortie = entry.IdBonSortie,
                    NbrE = entry.NbrE,
                    NbrS = entry.NbrS,
                    Date = entry.Date,
                    IdSociete = societeId,
                    IdProduit = productId,
                    TotalStock = stockByProduct[productId]
                });
            }

            return Ok(result);
        }

        [HttpGet("etatStock")]
        public IActionResult GetEtatStock(int societeId)
        {
            var etatStock = _dbContext.JournalCasiers
                .Where(j => j.Societe.Id == societeId)
                .GroupBy(j => j.IdProduit)
                .Select(g => new
                {
                    IdProduit = g.Key,
                    TotalNbrE = g.Sum(j => j.NbrE),
                    TotalNbrS = g.Sum(j => j.NbrS),
                    StockTotal = g.Sum(j => j.NbrE) - g.Sum(j => j.NbrS),
                    Produit = _dbContext.Produits.FirstOrDefault(p => p.Id == g.Key)
                })
                .ToList();

            return Ok(etatStock);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var entry = _dbContext.JournalCasiers
                .Include(j => j.BonEntree)
                .Include(j => j.BonSortie)
                .Include(j => j.Produit)
                .Include(j => j.Societe)
                .FirstOrDefault(j => j.Id == id);

            if (entry == null)
                return NotFound();

            return Ok(entry);
        }

        [HttpPost]
        public IActionResult AddItem(JournalCasierDto dto)
        {
            var entry = new JournalCasier
            {
                IdBonEntree = dto.IdBonEntree,
                IdBonSortie = dto.IdBonSortie,
                NbrE = dto.NbrE,
                NbrS = dto.NbrS,
                Date = dto.Date,
                IdSociete = dto.IdSociete,
                IdProduit = dto.IdProduit
            };

            _dbContext.JournalCasiers.Add(entry);
            _dbContext.SaveChanges();

            return Ok(entry);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, JournalCasierDto dto)
        {
            var entry = _dbContext.JournalCasiers.Find(id);
            if (entry == null)
                return NotFound();

            entry.IdBonEntree = dto.IdBonEntree;
            entry.IdBonSortie = dto.IdBonSortie;
            entry.NbrE = dto.NbrE;
            entry.NbrS = dto.NbrS;
            entry.Date = dto.Date;
            entry.IdSociete = dto.IdSociete;
            entry.IdProduit = dto.IdProduit;

            _dbContext.JournalCasiers.Update(entry);
            _dbContext.SaveChanges();

            return Ok(entry);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var entry = _dbContext.JournalCasiers.Find(id);
            if (entry == null)
                return NotFound();

            _dbContext.JournalCasiers.Remove(entry);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
