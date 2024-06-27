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

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbObj = _dbContext.BonSorties
                .Include(b => b.Client)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Include(b => b.Fournisseur)
                .FirstOrDefault(b => b.Id == id);

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
                QteE = 0, // Quantité ajoutée
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

            // Recherche d'une entrée existante dans le journal stock pour ce produit
            var existingEntry = _dbContext.JournalStock.FirstOrDefault(j => j.IdProduit == obj.IdProduit && j.IdBonSortie == dbObj.Id);

            if (existingEntry != null)
            {
                // Mise à jour de la quantité dans l'entrée existante du journal stock
                existingEntry.QteS = obj.Qte;
            }
            _dbContext.JournalStock.Update(existingEntry);
            _dbContext.SaveChanges();
            return Ok(dbObj);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbObj = _dbContext.BonSorties.Find(id);
            if (dbObj == null)
                return NotFound();

            _dbContext.BonSorties.Remove(dbObj);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
