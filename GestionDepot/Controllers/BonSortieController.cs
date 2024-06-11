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
            // Inclure les entités liées (Client, Produit, Chambre) lors de la récupération
            var allObjects = _dbContext.BonSorties
                .Include(b => b.Client)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .ToList();

            return Ok(allObjects);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            // Inclure les entités liées (Client, Produit, Chambre) lors de la récupération
            var dbObj = _dbContext.BonSorties
                .Include(b => b.Client)
                .Include(b => b.Produit)
                .Include(b => b.Chambre)
                .FirstOrDefault(b => b.Id == id);

            if (dbObj == null)
                return NotFound();
            else
                return Ok(dbObj);
        }

        [HttpPost]
        public IActionResult AddItem(BonSortieDto obj)
        {
            var dbObj = new BonSortie
            {
                Date = obj.Date,
                Qte = obj.Qte,
                IdChambre = obj.IdChambre,
                IdClient = obj.IdClient,
                IdProduit = obj.IdProduit,
                IdSociete = obj.IdSociete
            };

            _dbContext.BonSorties.Add(dbObj);
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

            _dbContext.BonSorties.Update(dbObj);
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
