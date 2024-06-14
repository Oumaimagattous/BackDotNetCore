using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migdd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSociete",
                table: "JournalStock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalStock_IdSociete",
                table: "JournalStock",
                column: "IdSociete");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_Societes_IdSociete",
                table: "JournalStock",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_Societes_IdSociete",
                table: "JournalStock");

            migrationBuilder.DropIndex(
                name: "IX_JournalStock_IdSociete",
                table: "JournalStock");

            migrationBuilder.DropColumn(
                name: "IdSociete",
                table: "JournalStock");
        }
    }
}
