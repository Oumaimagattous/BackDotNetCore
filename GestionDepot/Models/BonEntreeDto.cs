namespace GestionDepot.Models
{
    public class BonEntreeDto
    {
        public DateTime Date { get; set; }
        public decimal Qte { get; set; }

        public int? IdFournisseur { get; set; }
        public int? IdProduit { get; set; }
        public int? IdChambre { get; set; }
        public int? IdSociete { get; set; }
        public int NombreCasier { get; set; }



    }
}
