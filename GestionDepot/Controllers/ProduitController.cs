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
        [HttpGet("all")] // Nouvelle route spécifique pour GetAll
        public IActionResult GetAll()
        {
            var allProducts = dbcontext.Produits.ToList();
            return Ok(allProducts);
        }

        // Nouvelle méthode pour obtenir les produits par ID de société
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
            var dbobj = dbcontext.Produits.Find(id);
            if (dbobj == null)
                return NotFound();
            else
                return Ok(dbobj);

        }
        [HttpPost]
        public IActionResult AddItem(ProduitDto obj)
        {
            var existingProduct = dbcontext.Produits.FirstOrDefault(p => p.Name == obj.Name);

            if (existingProduct == null)
            {
                var newProduct = new Produit
                {
                    Name = obj.Name,
                    IdSociete = obj.IdSociete
                };

                dbcontext.Produits.Add(newProduct);
                dbcontext.SaveChanges();

                //// Ajouter une nouvelle entrée dans le journal stock pour le nouveau produit
                //var journalEntry = new JournalStock
                //{
                //    Date = DateTime.Now,
                //    QteE = 0, // Initialiser la quantité à 0
                //    QteS = 0,
                //    IdProduit = newProduct.Id
                //};

                //dbcontext.JournalStock.Add(journalEntry);
                //dbcontext.SaveChanges();

                return Ok(newProduct);
            }
            else
            {
                // Si le produit existe déjà, ne rien faire et renvoyer le produit existant
                return Ok(existingProduct);
            }

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