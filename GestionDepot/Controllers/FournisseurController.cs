using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public FournisseurController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Fournisseurs.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbobj = dbcontext.Fournisseurs.Find(id);
            if (dbobj == null)
                return NotFound();
            else
                return Ok(dbobj);

        }
        [HttpPost]
        public IActionResult AddItem(FournisseurDto obj)
        {
            var dbobj = new Fournisseur
            {
                Name = obj.Name,
                NomCommercial = obj.NomCommercial,
                CIN = obj.CIN,
                DateEmission = obj.DateEmission,
                Telephone = obj.Telephone,
                MF = obj.MF,
                Adresse = obj.Adresse,
                IdSociete=obj.IdSociete

            };

            dbcontext.Fournisseurs.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, FournisseurDto obj)
        {
            var dbobj = dbcontext.Fournisseurs.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name;
            dbobj.NomCommercial = obj.NomCommercial;
            dbobj.CIN = obj.CIN;
            dbobj.DateEmission = obj.DateEmission;
            dbobj.Telephone = obj.Telephone;
            dbobj.MF = obj.MF;
            dbobj.Adresse = obj.Adresse;
            dbobj.IdSociete = obj.IdSociete;

           dbcontext.Fournisseurs.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Fournisseurs.Find(id);
            if (dbobj is null)
                return NotFound();
            dbcontext.Fournisseurs.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }
}

