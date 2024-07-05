using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonEntreeController : ControllerBase
    {
        private readonly GestionDBContext _dbContext;


        public BonEntreeController(GestionDBContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObjects = _dbContext.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .ToList();

            return Ok(allObjects);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbObj = _dbContext.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .Single(b => b.Id == id);

            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }


        [HttpPost]
        public IActionResult AddItem(BonEntreeDto obj)
        {
            
            int numeroBonEntree = _dbContext.BonEntrees.Any() ? _dbContext.BonEntrees.Max(b => b.NumeroBonEntree) + 1 : 1;
            string numeroBonEntreeFormatted = "BE Num:" + numeroBonEntree;

            var dbObj = new BonEntree
            {
                Date = obj.Date,
                Qte = obj.Qte,
                IdChambre = obj.IdChambre,
                IdFournisseur = obj.IdFournisseur,
                IdProduit = obj.IdProduit,
                IdSociete = obj.IdSociete,
                NumeroBonEntree = numeroBonEntree,
                NombreCasier = obj.NombreCasier
            };


            _dbContext.BonEntrees.Add(dbObj);
            _dbContext.SaveChanges();

           
            var journalEntry = new JournalStock
            {
                Date = obj.Date,
                QteE = obj.Qte, 
                QteS = 0,
                IdProduit = obj.IdProduit,
                IdBonEntree = dbObj.Id,
                IdBonSortie = null,
                IdSociete = obj.IdSociete,
                IdFournisseur = obj.IdFournisseur,
                NumeroBon = numeroBonEntreeFormatted



            };

            _dbContext.JournalStock.Add(journalEntry);
         

            _dbContext.SaveChanges();

            var journalCasierEntry = new JournalCasier
            {
                Date = obj.Date,
                NbrE = obj.NombreCasier,
                NbrS = 0,
                IdProduit = obj.IdProduit,
                IdBonEntree = dbObj.Id,
                IdBonSortie = null,
                IdSociete = obj.IdSociete,
                IdFournisseur = obj.IdFournisseur
            };

            _dbContext.JournalCasiers.Add(journalCasierEntry);
            _dbContext.SaveChanges();

            return Ok(dbObj);


        }


        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, BonEntreeDto obj)
        {
            var dbObj = _dbContext.BonEntrees.Find(id);
            if (dbObj == null)
                return NotFound();

            dbObj.Date = obj.Date;
            dbObj.Qte = obj.Qte;
            dbObj.IdChambre = obj.IdChambre;
            dbObj.IdFournisseur = obj.IdFournisseur;
            dbObj.IdProduit = obj.IdProduit;
            dbObj.IdSociete = obj.IdSociete;
            dbObj.NombreCasier = obj.NombreCasier;

            _dbContext.BonEntrees.Update(dbObj);
            _dbContext.SaveChanges();

            var existingEntry = _dbContext.JournalStock.FirstOrDefault(j => j.IdProduit == obj.IdProduit && j.IdBonEntree == dbObj.Id);

            if (existingEntry != null)
            {
               
                existingEntry.QteE = obj.Qte;
                _dbContext.JournalStock.Update(existingEntry);
                _dbContext.SaveChanges();

            }

            var journalCasierEntry = _dbContext.JournalCasiers.SingleOrDefault(j => j.IdProduit == obj.IdProduit && j.IdBonEntree == dbObj.Id);
            if (journalCasierEntry != null)
            {
               
                journalCasierEntry.NbrE = obj.NombreCasier;

                _dbContext.JournalCasiers.Update(journalCasierEntry);
                _dbContext.SaveChanges();
            }

            return Ok();

        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dbObj = _dbContext.BonEntrees.SingleOrDefault(b => b.Id == id);
                if (dbObj == null)
                    return NotFound();

                _dbContext.BonEntrees.Remove(dbObj);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

    }
}
