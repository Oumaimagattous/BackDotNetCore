using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class JournalStockDto
    {
        public DateTime Date { get; set; }

        public decimal QteE { get; set; }
        public decimal QteS { get; set; }
        public decimal StockTotal { get; set; }
        public string NumeroBon { get; set; }

        public int? IdProduit { get; set; }
        public Produit Produit { get; set; }
        public int? IdBonSortie { get; set; }
        public int? IdBonEntree { get; set; }
        public int? IdSociete { get; set; }
        public int? IdFournisseur { get; set; }
        public Fournisseur Fournisseur { get; set; }



    }
}
