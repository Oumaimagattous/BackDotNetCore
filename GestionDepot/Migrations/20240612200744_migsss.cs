using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonSorties_Clients_IdClient",
                table: "BonSorties");

            migrationBuilder.AddColumn<string>(
                name: "CIN",
                table: "Fournisseurs",
                type: "nvarchar(16)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEmission",
                table: "Fournisseurs",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MF",
                table: "Fournisseurs",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomCommercial",
                table: "Fournisseurs",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Fournisseurs",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEmission",
                table: "Clients",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MF",
                table: "Clients",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Clients",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Chauffeur",
                table: "BonSorties",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CinChauffeur",
                table: "BonSorties",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdFournisseur",
                table: "BonSorties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Matricule",
                table: "BonSorties",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroBonSortie",
                table: "BonSorties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NombreCasier",
                table: "BonEntrees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroBonEntree",
                table: "BonEntrees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BonSorties_IdFournisseur",
                table: "BonSorties",
                column: "IdFournisseur");

            migrationBuilder.AddForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdClient",
                table: "BonSorties",
                column: "IdClient",
                principalTable: "Fournisseurs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdFournisseur",
                table: "BonSorties",
                column: "IdFournisseur",
                principalTable: "Fournisseurs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdClient",
                table: "BonSorties");

            migrationBuilder.DropForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdFournisseur",
                table: "BonSorties");

            migrationBuilder.DropIndex(
                name: "IX_BonSorties_IdFournisseur",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "CIN",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "DateEmission",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "MF",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "NomCommercial",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "DateEmission",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MF",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Chauffeur",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "CinChauffeur",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "IdFournisseur",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "Matricule",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "NumeroBonSortie",
                table: "BonSorties");

            migrationBuilder.DropColumn(
                name: "NombreCasier",
                table: "BonEntrees");

            migrationBuilder.DropColumn(
                name: "NumeroBonEntree",
                table: "BonEntrees");

            migrationBuilder.AddForeignKey(
                name: "FK_BonSorties_Clients_IdClient",
                table: "BonSorties",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
