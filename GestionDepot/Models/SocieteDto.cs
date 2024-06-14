using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class SocieteDto
    {
         
        public string Name { get; set; } = "";
         
        public string Adresse { get; set; } = "";
         
        public string MF { get; set; } = "";

        public string Telephone { get; set; } = "";
        public string Responsable { get; set; } = "";
        public string Email { get; set; } = "";

    }
}
