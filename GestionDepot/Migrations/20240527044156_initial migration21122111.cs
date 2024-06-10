using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration21122111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonEntrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Qte = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    IdFournisseur = table.Column<int>(type: "int", nullable: true),
                    IdProduit = table.Column<int>(type: "int", nullable: true),
                    IdChambre = table.Column<int>(type: "int", nullable: true),
                    IdSociete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonEntrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonEntrees_Chambres_IdChambre",
                        column: x => x.IdChambre,
                        principalTable: "Chambres",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonEntrees_Fournisseurs_IdFournisseur",
                        column: x => x.IdFournisseur,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonEntrees_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonEntrees_Societes_IdSociete",
                        column: x => x.IdSociete,
                        principalTable: "Societes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonEntrees_IdChambre",
                table: "BonEntrees",
                column: "IdChambre");

            migrationBuilder.CreateIndex(
                name: "IX_BonEntrees_IdFournisseur",
                table: "BonEntrees",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_BonEntrees_IdProduit",
                table: "BonEntrees",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_BonEntrees_IdSociete",
                table: "BonEntrees",
                column: "IdSociete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonEntrees");
        }
    }
}
