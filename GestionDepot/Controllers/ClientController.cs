﻿using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var item = dbcontext.Clients.Find(id);
            if (item == null)
                return NotFound();
            else
                return Ok(item);

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
                IdSociete=obj.IdSociete
     

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

