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

        public DbSet<JournalStock> JournalStock { get; set; }

        public DbSet<JournalCasier> JournalCasiers { get; set; }

        public DbSet<Login> Logins { get; set; }

        public void AddEntry(JournalStock entry)
        {
            JournalStock.Add(entry);
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("connection_string", options =>
                {
                    options.CommandTimeout(120); // Augmenter le délai d'attente à 120 secondes
                });
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BonEntree>()
                .Property(b => b.NumeroBonEntree)
                .ValueGeneratedNever(); // Empêche la génération automatique d'identité

            modelBuilder.Entity<BonSortie>()
              .Property(b => b.NumeroBonSortie)
              .ValueGeneratedNever(); // Empêche la génération automatique d'identité pour BonSortie

            // Autres configurations...
        }
    }
}
