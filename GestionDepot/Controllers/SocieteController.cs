using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var item = dbcontext.Societes.Find(id);
            if (item == null)
                return NotFound();
            else
                return Ok(item);

        }
        [HttpPost]
        public IActionResult AddItem(SocieteDto obj)
        {
            var dbobj = new Societe
            {
                Name = obj.Name,
                Adresse = obj.Adresse,
                MF = obj.MF,

            };

            dbcontext.Societes.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update(int id, SocieteDto obj)
        {
            var dbobj = dbcontext.Societes.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name;
            dbobj.Adresse = obj.Adresse;
            dbobj.MF = obj.MF;

            dbcontext.Societes.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Societes.Find(id);
            if (dbobj is null)
                return NotFound();

            dbcontext.Societes.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}

