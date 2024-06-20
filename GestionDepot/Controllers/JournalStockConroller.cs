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
            // Récupérer toutes les entrées de journal de stock pour la société spécifiée
            var allEntries = _dbContext.JournalStock
                .Include(js => js.Produit)
                .Include(js => js.BonEntree)
                .Include(js => js.BonSortie)
                .Include(js => js.Fournisseur)
                .Where(js => js.Produit.IdSociete == societeId)
                .OrderBy(js => js.Date)
                .ToList();

            // Dictionnaire pour suivre le stock total par produit et fournisseur
            var stockByProductAndSupplier = new Dictionary<(int, int), decimal>();

            // Liste des résultats à retourner
            var result = new List<JournalStockDto>();

            // Calculer le stock total pour chaque entrée
            foreach (var entry in allEntries)
            {
                // Utiliser l'opérateur de levée de null pour s'assurer que IdProduit et IdFournisseur ne sont pas null
                int productId = entry.IdProduit ?? 0; // Remplacer 0 par une valeur par défaut appropriée
                int supplierId = entry.IdFournisseur ?? 0; // Remplacer 0 par une valeur par défaut appropriée

                // Initialiser le stock pour le produit et le fournisseur s'ils n'existent pas encore dans le dictionnaire
                if (!stockByProductAndSupplier.ContainsKey((productId, supplierId)))
                {
                    stockByProductAndSupplier[(productId, supplierId)] = 0;
                }

                // Mettre à jour le stock total pour le produit et le fournisseur
                stockByProductAndSupplier[(productId, supplierId)] += entry.QteE - entry.QteS;

                // Ajouter l'entrée à la liste des résultats avec le stock total calculé
                result.Add(new JournalStockDto
                {
                    Date = entry.Date,
                    QteE = entry.QteE,
                    QteS = entry.QteS,
                    NumeroBon = entry.NumeroBon,
                    StockTotal = stockByProductAndSupplier[(productId, supplierId)],
                    IdProduit = productId,
                    Produit = entry.Produit,
                    IdBonEntree = entry.IdBonEntree,
                    IdBonSortie = entry.IdBonSortie,
                    IdSociete = societeId,
                    IdFournisseur = supplierId
                    
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
                .GroupBy(j => new { j.IdProduit, j.IdFournisseur })
                .Select(g => new
                {
                    IdProduit = g.Key.IdProduit,
                    IdFournisseur = g.Key.IdFournisseur,
                    TotalQteE = g.Sum(j => j.QteE),
                    TotalQteS = g.Sum(j => j.QteS),
                    StockTotal = g.Sum(j => j.QteE) - g.Sum(j => j.QteS),
                    Produit = _dbContext.Produits.FirstOrDefault(p => p.Id == g.Key.IdProduit),
                    Fournisseur = _dbContext.Fournisseurs.FirstOrDefault(f => f.Id == g.Key.IdFournisseur)
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
                .Include(b => b.Fournisseur)
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
                NumeroBon = obj.NumeroBon,
                IdProduit = obj.IdProduit,
                IdBonSortie = obj.IdBonSortie,
                IdBonEntree = obj.IdBonEntree,
                IdFournisseur = obj.IdFournisseur
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
            dbObj.NumeroBon = obj.NumeroBon;
            dbObj.IdProduit = obj.IdProduit;
            dbObj.IdBonEntree = obj.IdBonEntree;
            dbObj.IdBonSortie = obj.IdBonSortie;
            dbObj.IdFournisseur = obj.IdFournisseur;

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
