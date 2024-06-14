namespace GestionDepot.Models
{
    public class FournisseurDto
    {
        public string Name { get; set; }

        public string NomCommercial { get; set; }
        public string CIN { get; set; }
        public DateTime? DateEmission { get; set; }
        public string Telephone { get; set; }
        public string MF { get; set; }
        public string Adresse { get; set; }

        public int? IdSociete { get; set; }
    }
}
