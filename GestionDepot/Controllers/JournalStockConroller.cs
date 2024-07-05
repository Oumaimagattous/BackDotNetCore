using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            
            var allEntries = _dbContext.JournalStock
                .Include(js => js.Produit)
                .Include(js => js.BonEntree)
                .Include(js => js.BonSortie)
                .Include(js => js.Fournisseur)
                .Where(js => js.Produit.IdSociete == societeId)
                .OrderBy(js => js.Date)
                .ToList();

           
            var stockByProductAndSupplier = new Dictionary<(int, int), decimal>();

           
            var result = new List<JournalStockDto>();

          
            foreach (var entry in allEntries)
            {
                
                int productId = entry.IdProduit ?? 0; 
                int supplierId = entry.IdFournisseur ?? 0; 
                
                if (!stockByProductAndSupplier.ContainsKey((productId, supplierId)))
                {
                    stockByProductAndSupplier[(productId, supplierId)] = 0;
                }

                
                stockByProductAndSupplier[(productId, supplierId)] += entry.QteE - entry.QteS;

                
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


        [HttpGet("FiltrerParProduitEtFournisseur")]
        public IActionResult FiltrerParProduitEtFournisseur(int societeId, int idProduit, int idFournisseur, DateTime d1, DateTime d2)
        {
            var journalStockDtos = _dbContext.JournalStock
                .Include(js => js.Produit) 
                .Include(js => js.Fournisseur)
                .Where(js => js.IdSociete == societeId &&
                             js.IdProduit == idProduit &&
                             js.IdFournisseur== idFournisseur &&
                             js.Date >= d1 && js.Date<= d2
                            )
               

                .ToList();
            var stockByProductAndSupplier = new Dictionary<(int, int), decimal>();
            var result = new List<JournalStockDto>();

            foreach (var entry in journalStockDtos)
            {
                int productId = entry.IdProduit ?? 0;
                int supplierId = entry.IdFournisseur ?? 0;

                if (!stockByProductAndSupplier.ContainsKey((productId, supplierId)))
                {
                    stockByProductAndSupplier[(productId, supplierId)] = 0;
                }

                stockByProductAndSupplier[(productId, supplierId)] += entry.QteE - entry.QteS;

                result.Add(new JournalStockDto
                {
                    Date = entry.Date,
                    QteE = entry.QteE,
                    QteS = entry.QteS,
                    NumeroBon = entry.NumeroBon,
                    StockTotal = stockByProductAndSupplier[(productId, supplierId)],
                    IdBonEntree = entry.IdBonEntree,
                    IdBonSortie = entry.IdBonSortie,
                    IdProduit = productId,
                    Produit = entry.Produit

                });
            }

            return Ok(result);
        }



        //[HttpGet]
        //[Route("methode2")]
        //public IActionResult GetAll(int societeId,int idarticle,DateTime d1,DateTime d2 ,int idFour)
        //{
        //    Societe dbsociete = _dbContext.Societes.Single(a=>a.Id== societeId);

        //    List<JournalStock> liste = new List<JournalStock>();
        //    if(idFour!=0)
        //        liste = dbsociete.JournalStocks.Where(x=>x.IdProduit==idarticle && x.Date>=d1 && x.Date<=d2 && x.IdFournisseur==idFour).ToList();
        //    else
        //        liste = dbsociete.JournalStocks.Where(x => x.IdProduit == idarticle && x.Date >= d1 && x.Date <= d2).ToList();

        //    var stockByProductAndSupplier = new Dictionary<(int, int), decimal>();


        //    var result = new List<JournalStockDto>();


        //    foreach (var entry in liste)
        //    {

        //        int productId = entry.IdProduit ?? 0; 
        //        int supplierId = entry.IdFournisseur ?? 0; 

        //        if (!stockByProductAndSupplier.ContainsKey((productId, supplierId)))
        //        {
        //            stockByProductAndSupplier[(productId, supplierId)] = 0;
        //        }


        //        stockByProductAndSupplier[(productId, supplierId)] += entry.QteE - entry.QteS;


        //        result.Add(new JournalStockDto
        //        {
        //            Date = entry.Date,
        //            QteE = entry.QteE,
        //            QteS = entry.QteS,
        //            NumeroBon = entry.NumeroBon,
        //            StockTotal = stockByProductAndSupplier[(productId, supplierId)],
        //            IdProduit = productId,
        //            Produit = entry.Produit,
        //            IdBonEntree = entry.IdBonEntree,
        //            IdBonSortie = entry.IdBonSortie,
        //            IdSociete = societeId,
        //            IdFournisseur = supplierId

        //        });
        //    }

        //    return Ok(result);
        //}



        [HttpGet]
        [Route("methode3")]
        public IActionResult GetAll(int societeId, int idarticle, DateTime d1, DateTime d2, int idFour)
        {
            var query = _dbContext.JournalStock
                .Include(js => js.Produit)
                .Include(js => js.BonEntree)
                .Include(js => js.BonSortie)
                .Include(js => js.Fournisseur)
                .Where(js => js.Produit.IdSociete == societeId
                         && js.Date >= d1
                         && js.Date <= d2);

            if (idarticle != 0)
            {
                query = query.Where(js => js.IdProduit == idarticle);
            }

            if (idFour != 0)
            {
                query = query.Where(js => js.IdFournisseur == idFour);
            }

            var result = query.ToList()
                .GroupBy(js => new { js.IdProduit, js.IdFournisseur })
                .Select(group => new JournalStockDto
                {
                    Date = group.First().Date,
                    QteE = group.Sum(js => js.QteE),
                    QteS = group.Sum(js => js.QteS),
                    NumeroBon = group.First().NumeroBon,
                    StockTotal = group.Sum(js => js.QteE - js.QteS),
                    IdProduit = group.Key.IdProduit ?? 0,
                    Produit = group.First().Produit,
                    IdBonEntree = group.First().IdBonEntree,
                    IdBonSortie = group.First().IdBonSortie,
                    IdSociete = societeId,
                    IdFournisseur = group.Key.IdFournisseur ?? 0
                })
                .OrderBy(dto => dto.Date)
                .ToList();

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
            try
            {
                var dbObj = _dbContext.JournalStock
                    .Include(b => b.BonSortie)
                    .Include(b => b.Produit)
                    .Include(b => b.BonEntree)
                    .Include(b => b.Fournisseur)
                    .Single(b => b.Id == id);

                return Ok(dbObj);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
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
