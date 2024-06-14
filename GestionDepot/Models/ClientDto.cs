using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class ClientDto
    {
        
        public string Name { get; set; }           
        public string Adresse { get; set; }          
        public string Type { get; set; }

        public string Cin { get; set; }

        public string MF { get; set; }
        public string Telephone { get; set; }
        public DateTime? DateEmission { get; set; }
        public int? IdSociete { get; set; }
    }
}
