using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public ClientController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Clients.ToList();
            return Ok(Allobj);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            
            var item = dbcontext.Clients.Single(c => c.Id == id);
            return Ok(item);
        }


        [HttpGet("BySociete/{idSociete:int}")]
        public IActionResult GetBySocieteId(int idSociete)
        {
            var clients = dbcontext.Clients
                .Where(c => c.IdSociete == idSociete)
                .ToList();

            if (clients == null || clients.Count == 0)
                return NotFound();

            return Ok(clients);
        }

        [HttpPost]
        public IActionResult AddItem(ClientDto obj)
        {
            var dbobj = new Client
            {
                Name = obj.Name,
                Adresse = obj.Adresse,
                Type = obj.Type,
                Cin =obj.Cin,
                MF = obj.MF,
                Telephone = obj.Telephone,
                DateEmission = obj.DateEmission,
                IdSociete =obj.IdSociete
     

            };

            dbcontext.Clients.Add(dbobj);
            dbcontext.SaveChanges();
            
            
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, ClientDto obj)
        {
            var dbobj = dbcontext.Clients.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Name = obj.Name;
            dbobj.Adresse = obj.Adresse;
            dbobj.Type = obj.Type;
            dbobj.Cin = obj.Cin;
            dbobj.MF = obj.MF;
            dbobj.Telephone = obj.Telephone;
            dbobj.DateEmission = obj.DateEmission;
            dbobj.IdSociete = obj.IdSociete; 

            dbcontext.Clients.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Clients.Find(id);
            if (dbobj is null)
                return NotFound();
            dbcontext.Clients.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}


