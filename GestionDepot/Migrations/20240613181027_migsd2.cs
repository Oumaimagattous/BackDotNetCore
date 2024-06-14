using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migsd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JournalStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteE = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    QteS = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: true),
                    IdBonSortie = table.Column<int>(type: "int", nullable: true),
                    IdBonEntree = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalStock_BonEntrees_IdBonEntree",
                        column: x => x.IdBonEntree,
                        principalTable: "BonEntrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalStock_BonSorties_IdBonSortie",
                        column: x => x.IdBonSortie,
                        principalTable: "BonSorties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalStock_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalStock_IdBonEntree",
                table: "JournalStock",
                column: "IdBonEntree");

            migrationBuilder.CreateIndex(
                name: "IX_JournalStock_IdBonSortie",
                table: "JournalStock",
                column: "IdBonSortie");

            migrationBuilder.CreateIndex(
                name: "IX_JournalStock_IdProduit",
                table: "JournalStock",
                column: "IdProduit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalStock");
        }
    }
}
