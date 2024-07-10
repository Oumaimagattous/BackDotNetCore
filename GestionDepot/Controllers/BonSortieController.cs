using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonSortieController : ControllerBase
    {
        private readonly GestionDBContext _dbContext;

        public BonSortieController(GestionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObjects = _dbContext.BonSorties
                .Include(b => b.Client)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Fournisseur)
                .ToList();

            return Ok(allObjects);
        }
        //filtrer articles par fournisseur méthode 2
        [HttpGet]
        [Route("GetAllByFournuisseur/{idfour:int}")]
        public IActionResult GetAllByFournuisseur(int idfour)
        {
            var result = new List<Produit>();
               var grouped = _dbContext.JournalStock.Where(x => x.IdFournisseur == idfour).ToList().GroupBy(s => s.IdProduit).Select(group => new { somme = group.Sum(x => x.QteE - x.QteS), key = group.Key });
            foreach (var group in grouped.Where(a => a.somme > 0))
            {
                Produit p = _dbContext.Produits.Single(a=>a.Id==group.key);
                result.Add(p);
            }
           
            return Ok(result);
        }


        [HttpGet]
        [Route("GetChambreByFournisseurAndProduit/{idFournisseur:int}/{idProduit:int}")]
        public IActionResult GetChambreByFournisseurAndProduit(int idFournisseur, int idProduit)
        {
            var bonEntree = _dbContext.BonEntrees
                .Where(be => be.IdFournisseur == idFournisseur && be.IdProduit == idProduit)
                .OrderByDescending(be => be.Date) // Vous pouvez ajuster cet ordre selon vos besoins
                .FirstOrDefault();

            if (bonEntree == null)
                return NotFound("No matching Bon Entree found for the given fournisseur and produit.");

            return Ok(bonEntree.IdChambre);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbObj = _dbContext.BonSorties
                .Include(b => b.Client)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Fournisseur)
                .Single(b => b.Id == id);

            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }

        [HttpPost]
        public IActionResult AddItem(BonSortieDto obj)
        {
            int numeroBonSortie = _dbContext.BonSorties.Count() + 1;
            string numeroBonSortieFormatted = "BS Num:" + numeroBonSortie;

            var dbObj = new BonSortie
            {
                Date = obj.Date,
                Qte = obj.Qte,
                IdChambre = obj.IdChambre,
                IdClient = obj.IdClient,
                IdProduit = obj.IdProduit,
                IdSociete = obj.IdSociete,
                IdFournisseur = obj.IdFournisseur,
                Matricule = obj.Matricule,
                Chauffeur = obj.Chauffeur,
                CinChauffeur = obj.CinChauffeur,
                NumeroBonSortie = numeroBonSortie,
                NbrScasier = obj.NbrScasier
            };




            _dbContext.BonSorties.Add(dbObj);
            _dbContext.SaveChanges();


            var journalEntry = new JournalStock
            {
                Date = obj.Date,
                QteE = 0, 
                QteS = obj.Qte,
                IdProduit = obj.IdProduit,
                IdBonEntree = null,
                IdBonSortie = dbObj.Id,
                IdSociete = obj.IdSociete,
                IdFournisseur = obj.IdFournisseur,
                NumeroBon = numeroBonSortieFormatted


            };

            _dbContext.JournalStock.Add(journalEntry);

            _dbContext.SaveChanges();

            var journalCasierEntry = new JournalCasier
            {
                Date = obj.Date,
                NbrE = 0,
                NbrS = obj.NbrScasier,
                IdProduit = obj.IdProduit,
                IdBonEntree = null,
                IdBonSortie = dbObj.Id,
                IdSociete = obj.IdSociete,
                IdFournisseur = obj.IdFournisseur
            };

            _dbContext.JournalCasiers.Add(journalCasierEntry);

            _dbContext.SaveChanges();
            return Ok(dbObj);

            var chambreEntry = new Chambre
            {
                Name = "Chambre Exit",
                IdSociete = obj.IdSociete,
                IdProduit = obj.IdProduit,
                IdFournisseur = obj.IdFournisseur
            };

            _dbContext.Chambres.Add(chambreEntry);
            _dbContext.SaveChanges();

            return Ok(dbObj);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, BonSortieDto obj)
        {
            var dbObj = _dbContext.BonSorties.Find(id);
            if (dbObj == null)
                return NotFound();

            dbObj.Date = obj.Date;
            dbObj.Qte = obj.Qte;
            dbObj.IdChambre = obj.IdChambre;
            dbObj.IdClient = obj.IdClient;
            dbObj.IdProduit = obj.IdProduit;
            dbObj.IdSociete = obj.IdSociete;
            dbObj.IdFournisseur = obj.IdFournisseur;
            dbObj.Matricule = obj.Matricule;
            dbObj.Chauffeur = obj.Chauffeur;
            dbObj.CinChauffeur = obj.CinChauffeur;
            dbObj.NbrScasier = obj.NbrScasier;

            _dbContext.BonSorties.Update(dbObj);
            _dbContext.SaveChanges();

            
            var existingEntry = _dbContext.JournalStock.FirstOrDefault(j => j.IdProduit == obj.IdProduit && j.IdBonSortie == dbObj.Id);

            if (existingEntry != null)
            {
                
                existingEntry.QteS = obj.Qte;

                _dbContext.JournalStock.Update(existingEntry);
                _dbContext.SaveChanges();
            }
            

            var journalCasierEntry = _dbContext.JournalCasiers.SingleOrDefault(j => j.IdProduit == obj.IdProduit && j.IdBonSortie == dbObj.Id);
            if (journalCasierEntry != null)
            {

                journalCasierEntry.NbrS = obj.NbrScasier;

                _dbContext.JournalCasiers.Update(journalCasierEntry);
                _dbContext.SaveChanges();
            }


            return Ok(dbObj);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dbObj = _dbContext.BonSorties.SingleOrDefault(b => b.Id == id);
                if (dbObj == null)
                    return NotFound();

                _dbContext.BonSorties.Remove(dbObj);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        //[HttpGet]
        //[Route("GetProductsBySupplier/{fournisseurId:int}")]
        //public IActionResult GetProductsBySupplier(int fournisseurId)
        //{
        //    var produitsAvecStock = _dbContext.Produits
        //        .Include(p => p.JournalStocks)
        //        .Where(p => p.JournalStocks.Any(js => js.IdFournisseur == fournisseurId && (js.QteE - js.QteS) > 0))
        //        .Select(p => new
        //        {
        //            Produit = new
        //            {
        //                Id = p.Id,
        //                Name = p.Name,
        //                IdSociete = p.IdSociete
        //            },
        //            StockTotal = p.JournalStocks
        //                .Where(js => js.IdFournisseur == fournisseurId)
        //                .Sum(js => js.QteE - js.QteS)
        //        })
        //        .ToList();

        //    return Ok(produitsAvecStock);
        //}


    }
}
