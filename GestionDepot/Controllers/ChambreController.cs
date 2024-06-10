using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChambreController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public ChambreController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Chambres.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dbobj = dbcontext.Chambres.Find(id);
            if (dbobj == null)
                return NotFound();
            else
                return Ok(dbobj);

        }
        [HttpPost]
        public IActionResult AddItem(ChambreDto obj)
        {
            var dbobj = new Chambre
            {
                Name = obj.Name,
                IdSociete =obj.IdSociete
            };

            dbcontext.Chambres.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ChambreDto obj)
        {
            var dbobj = dbcontext.Chambres.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name;
            dbobj.IdSociete = obj.IdSociete;

            dbcontext.Chambres.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Chambres.Find(id);
            if (dbobj is null)
                return NotFound();

            dbcontext.Chambres.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}