using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalStockController : ControllerBase
    {
        private readonly GestionDBContext _dbContext;

        public JournalStockController(GestionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(int societeId)
        {
            // Récupérer toutes les entrées de journal de stock
            var allEntries = _dbContext.JournalStock
                .Include(js => js.Produit)
                .Include(js => js.BonEntree)
                .Include(js => js.BonSortie)
                .Where(js => js.IdSociete == societeId)
                .OrderBy(js => js.Date)
                .ToList();

            // Dictionnaire pour suivre le stock total par produit
            var stockByProduct = new Dictionary<int, decimal>();

            // Liste des résultats à retourner
            var result = new List<JournalStockDto>();

            // Calculer le stock total pour chaque entrée
            foreach (var entry in allEntries)
            {
                // Utiliser l'opérateur de levée de null pour s'assurer que IdProduit n'est pas null
                int productId = entry.IdProduit ?? 0; // Remplacer 0 par une valeur par défaut appropriée

                // Initialiser le stock pour le produit s'il n'existe pas encore dans le dictionnaire
                if (!stockByProduct.ContainsKey(productId))
                {
                    stockByProduct[productId] = 0;
                }

                // Mettre à jour le stock total pour le produit
                stockByProduct[productId] += entry.QteE - entry.QteS;

                // Ajouter l'entrée à la liste des résultats avec le stock total calculé
                result.Add(new JournalStockDto
                {
                    Date = entry.Date,
                    QteE = entry.QteE,
                    QteS = entry.QteS,
                    StockTotal = stockByProduct[productId],
                    IdProduit = productId,
                    Produit = entry.Produit,
                    IdBonEntree = entry.IdBonEntree,
                    IdBonSortie = entry.IdBonSortie,
                    IdSociete = societeId
                });
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("etatStock")]
        public IActionResult GetEtatStock(int societeId)
        {
            var etatStock = _dbContext.JournalStock
                .Where(j => j.Produit.IdSociete == societeId)
                .GroupBy(j => j.IdProduit)
                .Select(g => new
                {
                    IdProduit = g.Key,
                    TotalQteE = g.Sum(j => j.QteE),
                    TotalQteS = g.Sum(j => j.QteS),
                    StockTotal = g.Sum(j => j.QteE) - g.Sum(j => j.QteS),
                    Produit = _dbContext.Produits.FirstOrDefault(p => p.Id == g.Key)
                })
                .ToList();

            return Ok(etatStock);
        }



        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbObj = _dbContext.JournalStock
                .Include(b => b.BonSortie)
                .Include(b => b.Produit)
                .Include(b => b.BonEntree)
                .FirstOrDefault(b => b.Id == id);

            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }

        [HttpPost]
        public IActionResult AddItem(JournalStockDto obj)
        {
            var dbObj = new JournalStock
            {
                Date = obj.Date,
                QteE = obj.QteE,
                QteS = obj.QteS,
                IdProduit = obj.IdProduit,
                IdBonSortie = obj.IdBonSortie,
                IdBonEntree = obj.IdBonEntree
            };

            _dbContext.JournalStock.Add(dbObj);
            _dbContext.SaveChanges();
            return Ok(dbObj);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, JournalStockDto obj)
        {
            var dbObj = _dbContext.JournalStock.Find(id);
            if (dbObj == null)
                return NotFound();

            dbObj.Date = obj.Date;
            dbObj.QteE = obj.QteE;
            dbObj.QteS = obj.QteS;
            dbObj.IdProduit = obj.IdProduit;
            dbObj.IdBonEntree = obj.IdBonEntree;
            dbObj.IdBonSortie = obj.IdBonSortie;

            _dbContext.JournalStock.Update(dbObj);
            _dbContext.SaveChanges();
            return Ok(dbObj);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbObj = _dbContext.JournalStock.Find(id);
            if (dbObj == null)
                return NotFound();

            _dbContext.JournalStock.Remove(dbObj);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
