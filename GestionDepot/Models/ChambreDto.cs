namespace GestionDepot.Models
{
    public class ChambreDto
    {
        public string Name { get; set; } = "";

        public int? IdSociete { get; set; }
        public int? IdProduit { get; set; }
        public decimal TotalStock { get; set; }
        public int? IdFournisseur { get; set; }
       
    }
}
