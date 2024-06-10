using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class LoginDto
    {
        
        public string Email { get; set; }           
        public string Password { get; set; }

        public int? IdSociete { get; set; }
    }
}
