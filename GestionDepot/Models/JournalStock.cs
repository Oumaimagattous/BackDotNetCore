using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class JournalStock
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal QteE { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal QteS { get; set; }

        public string NumeroBon { get; set; }
        

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }


        [ForeignKey("Produit")]
        public int? IdProduit { get; set; }
        public Produit Produit { get; set; }

        [ForeignKey("BonSortie")]
        public int? IdBonSortie { get; set; }
        public BonSortie BonSortie { get; set; }

        [ForeignKey("BonEntree")]
        public int? IdBonEntree { get; set; }
        public BonEntree BonEntree { get; set; }

        [ForeignKey("Societe")]
        public int? IdSociete { get; set; }
        public Societe Societe { get; set; }

        [ForeignKey("Fournisseur")]
        public int? IdFournisseur { get; set; }
        public Fournisseur Fournisseur { get; set; }




    }
}
