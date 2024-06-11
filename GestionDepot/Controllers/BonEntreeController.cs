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
            // Inclure les entités liées (Fournisseur, Produit, Chambre) lors de la récupération
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
            // Inclure les entités liées (Fournisseur, Produit, Chambre) lors de la récupération
            var dbObj = _dbContext.BonEntrees
                .Include(b => b.Fournisseur)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .FirstOrDefault(b => b.Id == id);

            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }

        [HttpPost]
        public IActionResult AddItem(BonEntreeDto obj)
        {
            var dbObj = new BonEntree
            {
                Date = obj.Date,
                Qte = obj.Qte,
                IdChambre = obj.IdChambre,
                IdFournisseur = obj.IdFournisseur,
                IdProduit = obj.IdProduit,
                IdSociete = obj.IdSociete
            };

            _dbContext.BonEntrees.Add(dbObj);
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

            _dbContext.BonEntrees.Update(dbObj);
            _dbContext.SaveChanges();
            return Ok(dbObj);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbObj = _dbContext.BonEntrees.Find(id);
            if (dbObj == null)
                return NotFound();

            _dbContext.BonEntrees.Remove(dbObj);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
