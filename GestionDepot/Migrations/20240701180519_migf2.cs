using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migf2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalCasiers_BonSorties_IdBonSortie",
                table: "JournalCasiers");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_BonSorties_IdBonSortie",
                table: "JournalStock");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalCasiers_BonSorties_IdBonSortie",
                table: "JournalCasiers",
                column: "IdBonSortie",
                principalTable: "BonSorties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_BonSorties_IdBonSortie",
                table: "JournalStock",
                column: "IdBonSortie",
                principalTable: "BonSorties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalCasiers_BonSorties_IdBonSortie",
                table: "JournalCasiers");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_BonSorties_IdBonSortie",
                table: "JournalStock");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalCasiers_BonSorties_IdBonSortie",
                table: "JournalCasiers",
                column: "IdBonSortie",
                principalTable: "BonSorties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_BonSorties_IdBonSortie",
                table: "JournalStock",
                column: "IdBonSortie",
                principalTable: "BonSorties",
                principalColumn: "Id");
        }
    }
}
