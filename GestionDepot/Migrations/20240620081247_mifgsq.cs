using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class mifgsq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFournisseur",
                table: "JournalStock",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdFournisseur",
                table: "JournalCasiers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalStock_IdFournisseur",
                table: "JournalStock",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_JournalCasiers_IdFournisseur",
                table: "JournalCasiers",
                column: "IdFournisseur");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalCasiers_Fournisseurs_IdFournisseur",
                table: "JournalCasiers",
                column: "IdFournisseur",
                principalTable: "Fournisseurs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_Fournisseurs_IdFournisseur",
                table: "JournalStock",
                column: "IdFournisseur",
                principalTable: "Fournisseurs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalCasiers_Fournisseurs_IdFournisseur",
                table: "JournalCasiers");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_Fournisseurs_IdFournisseur",
                table: "JournalStock");

            migrationBuilder.DropIndex(
                name: "IX_JournalStock_IdFournisseur",
                table: "JournalStock");

            migrationBuilder.DropIndex(
                name: "IX_JournalCasiers_IdFournisseur",
                table: "JournalCasiers");

            migrationBuilder.DropColumn(
                name: "IdFournisseur",
                table: "JournalStock");

            migrationBuilder.DropColumn(
                name: "IdFournisseur",
                table: "JournalCasiers");
        }
    }
}
