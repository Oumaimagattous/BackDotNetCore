using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDepot.Models
{
    public class Fournisseur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";


        [Column(TypeName = "nvarchar(100)")]
        public string NomCommercial { get; set; } = "";

        [Column(TypeName = "nvarchar(16)")]
        public string CIN { get; set; } = "";

        [Column(TypeName = "date")]
        public DateTime? DateEmission { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Telephone { get; set; } = "";

        [Column(TypeName = "nvarchar(20)")]
        public string MF { get; set; } = "";

        [Column(TypeName = "nvarchar(16)")]
        public string Adresse { get; set; } = "";

        [ForeignKey("Societe")]
        public int? IdSociete { get; set; }
        public Societe Societe { get; set; }
    }
}
