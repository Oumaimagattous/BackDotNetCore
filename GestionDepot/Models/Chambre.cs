using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionDepot.Models
{
    public class Chambre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; } = "";


        [ForeignKey("Societe")]
        public int? IdSociete { get; set; }
        public Societe Societe { get; set; }

        [ForeignKey("Produit")]
        public int? IdProduit { get; set; }
        public Produit Produit { get; set; }

        [ForeignKey("Fournisseur")]
        public int? IdFournisseur { get; set; }
        public Fournisseur Fournisseur { get; set; }

        [JsonIgnore]
        public ICollection<BonEntree> BonEntrees { get; set; }
        [JsonIgnore]
        public ICollection<BonSortie> BonSorties { get; set; }

    }
}
