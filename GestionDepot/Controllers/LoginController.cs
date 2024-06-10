using GestionDepot.Data;
using GestionDepot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDepot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GestionDBContext dbcontext;
        public LoginController(GestionDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Allobj = dbcontext.Logins.ToList();
            return Ok(Allobj);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = dbcontext.Logins .Find(id);
            if (item == null)
                return NotFound();
            else
                return Ok(item);

        }
        [HttpPost]
        public IActionResult AddItem(LoginDto obj)
        {
            var dbobj = new Login
            {
                Email = obj.Email,  
                Password = obj.Password,
                IdSociete = obj.IdSociete,

            };

            dbcontext.Logins.Add(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);


        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, LoginDto obj)
        {
            var dbobj = dbcontext.Logins.Find(id);
            if (dbobj is null)
                return NotFound();

            dbobj.Email = obj.Email;
            dbobj.Password = obj.Password;
            dbobj.IdSociete = obj.IdSociete;

            dbcontext.Logins.Update(dbobj);
            dbcontext.SaveChanges();
            return Ok(dbobj);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dbobj = dbcontext.Logins.Find(id);
            if (dbobj is null)
                return NotFound();
            dbcontext.Logins.Remove(dbobj);
            dbcontext.SaveChanges();
            return Ok();
        }
    }

}


