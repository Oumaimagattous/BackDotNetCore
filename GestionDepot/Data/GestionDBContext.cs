using GestionDepot.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GestionDepot.Data
{
    public class GestionDBContext : DbContext
    {
        public GestionDBContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Societe> Societes { get; set; }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Chambre> Chambres { get; set; }

        public DbSet<BonEntree> BonEntrees { get; set; }

        public DbSet<BonSortie> BonSorties { get; set; }

        public DbSet<Login> Logins { get; set; }
    }
}
