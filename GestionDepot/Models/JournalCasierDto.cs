namespace GestionDepot.Models
{
    public class JournalCasierDto
    {
        public int? IdBonEntree { get; set; }
        public int? IdBonSortie { get; set; }
        public int NbrE { get; set; }
        public int NbrS { get; set; }
        public DateTime Date { get; set; }
        public int? IdSociete { get; set; }
        public int? IdProduit { get; set; }
        public int TotalStock { get; set; }
    }
}
