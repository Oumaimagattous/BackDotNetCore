using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public ProduitController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Produits.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbobj = dbcontext.Produits.Find(id);
            if (dbobj == null)
                return NotFound();
            else
                return Ok(dbobj);

        }
        [HttpPost]
        public IActionResult AddItem(ProduitDto obj)
        {
            var dbobj = new Produit
            {
                Name = obj.Name, 
                IdSociete=obj.IdSociete

            };

            dbcontext.Produits.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ClientDto obj)
        {
            var dbobj = dbcontext.Produits.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name; 
            dbobj.IdSociete=obj.IdSociete;

            dbcontext.Produits.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Produits.Find(id);
            if (dbobj is null)
                return NotFound();
            dbcontext.Produits.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}