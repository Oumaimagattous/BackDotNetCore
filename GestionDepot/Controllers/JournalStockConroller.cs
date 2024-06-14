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
        public IActionResult GetAll()
        {
            var allObjects = _dbContext.JournalStock
                .Include(b => b.BonSortie)
                .Include(b => b.Produit)
                .Include(b => b.BonEntree)
                .ToList();

            return Ok(allObjects);
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
