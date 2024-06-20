using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class JournalCasier
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BonEntree")]
        public int? IdBonEntree { get; set; }
        public BonEntree BonEntree { get; set; }

        [ForeignKey("BonSortie")]
        public int? IdBonSortie { get; set; }
        public BonSortie BonSortie { get; set; }

        public int NbrE { get; set; } // Nombre de casiers entrés

        public int NbrS { get; set; } // Nombre de casiers sortis

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [ForeignKey("Societe")]
        public int? IdSociete { get; set; }
        public Societe Societe { get; set; }

        [ForeignKey("Produit")]
        public int? IdProduit { get; set; }
        public Produit Produit { get; set; }

        [ForeignKey("Fournisseur")]
        public int? IdFournisseur { get; set; }
        public Fournisseur Fournisseur { get; set; }

    }
}
