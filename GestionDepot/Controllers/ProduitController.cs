using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("all")] 
        public IActionResult GetAll()
        {
            var allProducts = dbcontext.Produits.ToList();
            return Ok(allProducts);
        }

      
        [HttpGet("bysociete/{societeId:int}")]
        public IActionResult GetBySocieteId(int societeId)
        {
            var products = dbcontext.Produits
                                    .Where(p => p.IdSociete == societeId)
                                    .ToList();

            if (!products.Any())
                return NotFound("Aucun produit trouvé pour cette société.");

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbobj = dbcontext.Produits.Single(p => p.Id == id);
            return Ok(dbobj);
        }


        [HttpPost]
        public IActionResult AddItem(ProduitDto obj)
        {
            var existingProduct = dbcontext.Produits
                .SingleOrDefault(p => p.Name == obj.Name && p.IdSociete == obj.IdSociete);

            if (existingProduct == null)
            {
                var newProduct = new Produit
                {
                    Name = obj.Name,
                    IdSociete = obj.IdSociete
                };

                dbcontext.Produits.Add(newProduct);
                dbcontext.SaveChanges();

                return Ok(newProduct);
            }
            else
            {
               
                return Ok(existingProduct);
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ProduitDto obj)
        {
            try
            {
                var dbobj = dbcontext.Produits.Single(p => p.Id == id);

                // Vérifiez si la société existe
                var societeExists = dbcontext.Societes.Any(s => s.Id == obj.IdSociete);
                if (!societeExists)
                {
                    return BadRequest("La société spécifiée n'existe pas.");
                }

                dbobj.Name = obj.Name;
                dbobj.IdSociete = obj.IdSociete;

                dbcontext.Produits.Update(dbobj);
                dbcontext.SaveChanges();
                return Ok(dbobj);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dbobj = dbcontext.Produits.Single(p => p.Id == id);
                dbcontext.Produits.Remove(dbobj);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

    }

}