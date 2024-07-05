using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocieteController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public SocieteController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Societes.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = dbcontext.Societes.Single(s => s.Id == id);
                return Ok(item);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddItem(SocieteDto obj)
        {
            var dbobj = new Societe
            {
                Name = obj.Name,
                Adresse = obj.Adresse,
                MF = obj.MF,
                Telephone = obj.Telephone,
                Responsable = obj.Responsable,
                Email = obj.Email

            };

            dbcontext.Societes.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, SocieteDto obj)
        {
            try
            {
                var dbobj = dbcontext.Societes.Single(s => s.Id == id);

                dbobj.Name = obj.Name;
                dbobj.Adresse = obj.Adresse;
                dbobj.MF = obj.MF;
                dbobj.Telephone = obj.Telephone;
                dbobj.Responsable = obj.Responsable;
                dbobj.Email = obj.Email;

                dbcontext.Societes.Update(dbobj);
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
                var dbobj = dbcontext.Societes.Single(s => s.Id == id);

                dbcontext.Societes.Remove(dbobj);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

       
        [HttpGet("current")]
        public IActionResult GetCurrentSociete()
        {
            // Récupérer l'ID de la société connectée à partir du jeton JWT ou d'une autre source
            var societeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (societeIdClaim == null)
                return NotFound("Societe ID not found in token");

            var societeId = societeIdClaim.Value;

            // Ensuite, utiliser cet ID pour récupérer les informations de la société connectée
            var societe = dbcontext.Societes.Find(int.Parse(societeId)); 

            if (societe == null)
                return NotFound();
            else
                return Ok(societe);
        }
    }

}

