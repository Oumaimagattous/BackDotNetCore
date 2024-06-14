﻿// <auto-generated />
using System;
using GestionDepot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionDepot.Migrations
{
    [DbContext(typeof(GestionDBContext))]
    [Migration("20240614120825_migdd3")]
    partial class migdd3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionDepot.Models.BonEntree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int?>("IdChambre")
                        .HasColumnType("int");

                    b.Property<int?>("IdFournisseur")
                        .HasColumnType("int");

                    b.Property<int?>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<int>("NombreCasier")
                        .HasColumnType("int");

                    b.Property<int>("NumeroBonEntree")
                        .HasColumnType("int");

                    b.Property<decimal>("Qte")
                        .HasColumnType("decimal(16,3)");

                    b.HasKey("Id");

                    b.HasIndex("IdChambre");

                    b.HasIndex("IdFournisseur");

                    b.HasIndex("IdProduit");

                    b.HasIndex("IdSociete");

                    b.ToTable("BonEntrees");
                });

            modelBuilder.Entity("GestionDepot.Models.BonSortie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Chauffeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CinChauffeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int?>("IdChambre")
                        .HasColumnType("int");

                    b.Property<int?>("IdClient")
                        .HasColumnType("int");

                    b.Property<int?>("IdFournisseur")
                        .HasColumnType("int");

                    b.Property<int?>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("Matricule")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NumeroBonSortie")
                        .HasColumnType("int");

                    b.Property<decimal>("Qte")
                        .HasColumnType("decimal(16,3)");

                    b.HasKey("Id");

                    b.HasIndex("IdChambre");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdFournisseur");

                    b.HasIndex("IdProduit");

                    b.HasIndex("IdSociete");

                    b.ToTable("BonSorties");
                });

            modelBuilder.Entity("GestionDepot.Models.Chambre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdSociete");

                    b.ToTable("Chambres");
                });

            modelBuilder.Entity("GestionDepot.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cin")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateEmission")
                        .HasColumnType("date");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("MF")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdSociete");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("GestionDepot.Models.Fournisseur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("CIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime?>("DateEmission")
                        .HasColumnType("date");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("MF")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NomCommercial")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("IdSociete");

                    b.ToTable("Fournisseurs");
                });

            modelBuilder.Entity("GestionDepot.Models.JournalStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int?>("IdBonEntree")
                        .HasColumnType("int");

                    b.Property<int?>("IdBonSortie")
                        .HasColumnType("int");

                    b.Property<int?>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<decimal>("QteE")
                        .HasColumnType("decimal(16,3)");

                    b.Property<decimal>("QteS")
                        .HasColumnType("decimal(16,3)");

                    b.HasKey("Id");

                    b.HasIndex("IdBonEntree");

                    b.HasIndex("IdBonSortie");

                    b.HasIndex("IdProduit");

                    b.HasIndex("IdSociete");

                    b.ToTable("JournalStock");
                });

            modelBuilder.Entity("GestionDepot.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdSociete");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("GestionDepot.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdSociete")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdSociete");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("GestionDepot.Models.Societe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MF")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Responsable")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Societes");
                });

            modelBuilder.Entity("GestionDepot.Models.BonEntree", b =>
                {
                    b.HasOne("GestionDepot.Models.Chambre", "Chambre")
                        .WithMany()
                        .HasForeignKey("IdChambre");

                    b.HasOne("GestionDepot.Models.Fournisseur", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("IdFournisseur");

                    b.HasOne("GestionDepot.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit");

                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("Chambre");

                    b.Navigation("Fournisseur");

                    b.Navigation("Produit");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.BonSortie", b =>
                {
                    b.HasOne("GestionDepot.Models.Chambre", "Chambre")
                        .WithMany()
                        .HasForeignKey("IdChambre");

                    b.HasOne("GestionDepot.Models.Fournisseur", "Client")
                        .WithMany()
                        .HasForeignKey("IdClient");

                    b.HasOne("GestionDepot.Models.Fournisseur", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("IdFournisseur");

                    b.HasOne("GestionDepot.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit");

                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("Chambre");

                    b.Navigation("Client");

                    b.Navigation("Fournisseur");

                    b.Navigation("Produit");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Chambre", b =>
                {
                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Client", b =>
                {
                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany("Clients")
                        .HasForeignKey("IdSociete");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Fournisseur", b =>
                {
                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany("Fournisseurs")
                        .HasForeignKey("IdSociete");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.JournalStock", b =>
                {
                    b.HasOne("GestionDepot.Models.BonEntree", "BonEntree")
                        .WithMany()
                        .HasForeignKey("IdBonEntree");

                    b.HasOne("GestionDepot.Models.BonSortie", "BonSortie")
                        .WithMany()
                        .HasForeignKey("IdBonSortie");

                    b.HasOne("GestionDepot.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit");

                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("BonEntree");

                    b.Navigation("BonSortie");

                    b.Navigation("Produit");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Login", b =>
                {
                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Produit", b =>
                {
                    b.HasOne("GestionDepot.Models.Societe", "Societe")
                        .WithMany()
                        .HasForeignKey("IdSociete");

                    b.Navigation("Societe");
                });

            modelBuilder.Entity("GestionDepot.Models.Societe", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Fournisseurs");
                });
#pragma warning restore 612, 618
        }
    }
}
