using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class BonSortie
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal Qte { get; set; }

        [ForeignKey("Client")]
        public int? IdClient { get; set; }
        public Fournisseur Client { get; set; }


        [ForeignKey("Produit")]
        public int? IdProduit { get; set; }
        public Produit Produit { get; set; }


        [ForeignKey("Chambre")]
        public int? IdChambre { get; set; }
        public Chambre Chambre { get; set; }


        [ForeignKey("Societe")]
        public int? IdSociete { get; set; }
        public Societe Societe { get; set; }

        [ForeignKey("Fournisseur")]
        public int? IdFournisseur { get; set; }
        public Fournisseur Fournisseur { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Matricule { get; set; } = "";

        [Column(TypeName = "nvarchar(255)")]
        public string Chauffeur { get; set; } = "";

        [Column(TypeName = "nvarchar(255)")]
        public string CinChauffeur { get; set; } = "";

        public int NumeroBonSortie { get; set; }

        public int NbrScasier { get; set; }



    }
}
