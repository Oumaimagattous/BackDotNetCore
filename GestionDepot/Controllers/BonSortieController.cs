using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonSortieController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public BonSortieController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.BonSorties.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbobj = dbcontext.BonSorties.Find(id);
            if (dbobj == null)
                return NotFound();
            else
                return Ok(dbobj);

        }
        [HttpPost]
        public IActionResult AddItem(BonSortieDto obj)
        {
            var dbobj = new BonSortie
            {
                Date = obj.Date,
            Qte = obj.Qte,
            IdChambre = obj.IdChambre,
            IdClient = obj.IdClient,
            IdProduit = obj.IdProduit,
            IdSociete = obj.IdSociete
        };

            dbcontext.BonSorties.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, BonSortieDto obj)
        {
            var dbobj = dbcontext.BonSorties.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Date = obj.Date;
            dbobj.Qte = obj.Qte;
            dbobj.IdChambre=obj.IdChambre;  
            dbobj.IdClient = obj.IdClient;    
            dbobj.IdProduit = obj.IdProduit;    
            dbobj.IdSociete=obj.IdSociete; 

            dbcontext.BonSorties.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.BonSorties.Find(id);
            if (dbobj is null)
                return NotFound();

            dbcontext.BonSorties.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}