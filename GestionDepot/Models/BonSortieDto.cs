using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class BonSortieDto
    {

        public DateTime Date { get; set; }

        public decimal Qte { get; set; }

        public int? IdClient { get; set; }

        public int? IdProduit { get; set; }

        public int? IdChambre { get; set; }
        public int? IdSociete { get; set; }
    }
}
