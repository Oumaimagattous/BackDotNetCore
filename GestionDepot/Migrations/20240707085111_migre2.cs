using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migre2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFournisseur",
                table: "Chambres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProduit",
                table: "Chambres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_IdFournisseur",
                table: "Chambres",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_IdProduit",
                table: "Chambres",
                column: "IdProduit");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Fournisseurs_IdFournisseur",
                table: "Chambres",
                column: "IdFournisseur",
                principalTable: "Fournisseurs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Produits_IdProduit",
                table: "Chambres",
                column: "IdProduit",
                principalTable: "Produits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Fournisseurs_IdFournisseur",
                table: "Chambres");

            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Produits_IdProduit",
                table: "Chambres");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_IdFournisseur",
                table: "Chambres");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_IdProduit",
                table: "Chambres");

            migrationBuilder.DropColumn(
                name: "IdFournisseur",
                table: "Chambres");

            migrationBuilder.DropColumn(
                name: "IdProduit",
                table: "Chambres");
        }
    }
}
