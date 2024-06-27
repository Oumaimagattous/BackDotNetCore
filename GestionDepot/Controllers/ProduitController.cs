﻿using GestionDepot.Data;
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
            var existingProduct = dbcontext.Produits
                .FirstOrDefault(p => p.Name == obj.Name && p.IdSociete == obj.IdSociete);

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
                // Si le produit existe déjà pour cette société, ne rien faire et renvoyer le produit existant
                return Ok(existingProduct);
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ProduitDto obj)
        {
            var dbobj = dbcontext.Produits.Find(id);
            if (dbobj == null)
                return NotFound();

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