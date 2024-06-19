using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NbrScasier",
                table: "BonSorties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JournalCasiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBonEntree = table.Column<int>(type: "int", nullable: true),
                    IdBonSortie = table.Column<int>(type: "int", nullable: true),
                    NbrE = table.Column<int>(type: "int", nullable: false),
                    NbrS = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    IdSociete = table.Column<int>(type: "int", nullable: true),
                    IdProduit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalCasiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalCasiers_BonEntrees_IdBonEntree",
                        column: x => x.IdBonEntree,
                        principalTable: "BonEntrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalCasiers_BonSorties_IdBonSortie",
                        column: x => x.IdBonSortie,
                        principalTable: "BonSorties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalCasiers_Produits_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JournalCasiers_Societes_IdSociete",
                        column: x => x.IdSociete,
                        principalTable: "Societes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalCasiers_IdBonEntree",
                table: "JournalCasiers",
                column: "IdBonEntree");

            migrationBuilder.CreateIndex(
                name: "IX_JournalCasiers_IdBonSortie",
                table: "JournalCasiers",
                column: "IdBonSortie");

            migrationBuilder.CreateIndex(
                name: "IX_JournalCasiers_IdProduit",
                table: "JournalCasiers",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_JournalCasiers_IdSociete",
                table: "JournalCasiers",
                column: "IdSociete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalCasiers");

            migrationBuilder.DropColumn(
                name: "NbrScasier",
                table: "BonSorties");
        }
    }
}
