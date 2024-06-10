using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initial_mig11112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MF",
                table: "Societes",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)");

            migrationBuilder.CreateTable(
                name: "BonSorties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Qte = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: true),
                    IdProduit = table.Column<int>(type: "int", nullable: true),
                    IdChambre = table.Column<int>(type: "int", nullable: true),
                    IdSociete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonSorties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonSorties_Chambres_IdChambre",
                        column: x => x.IdChambre,
                        principalTable: "Chambres",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonSorties_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonSorties_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonSorties_Societes_IdSociete",
                        column: x => x.IdSociete,
                        principalTable: "Societes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonSorties_IdChambre",
                table: "BonSorties",
                column: "IdChambre");

            migrationBuilder.CreateIndex(
                name: "IX_BonSorties_IdClient",
                table: "BonSorties",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_BonSorties_IdProduit",
                table: "BonSorties",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_BonSorties_IdSociete",
                table: "BonSorties",
                column: "IdSociete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonSorties");

            migrationBuilder.AlterColumn<string>(
                name: "MF",
                table: "Societes",
                type: "nvarchar(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }
    }
}
