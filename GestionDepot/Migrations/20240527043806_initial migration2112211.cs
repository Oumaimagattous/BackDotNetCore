using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration2112211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSociete",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSociete",
                table: "Chambres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_IdSociete",
                table: "Produits",
                column: "IdSociete");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_IdSociete",
                table: "Chambres",
                column: "IdSociete");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Societes_IdSociete",
                table: "Chambres",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Societes_IdSociete",
                table: "Produits",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Societes_IdSociete",
                table: "Chambres");

            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Societes_IdSociete",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_IdSociete",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_IdSociete",
                table: "Chambres");

            migrationBuilder.DropColumn(
                name: "IdSociete",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "IdSociete",
                table: "Chambres");
        }
    }
}
