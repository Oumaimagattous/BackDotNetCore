using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migf442 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonSorties_Clients_IdClient",
                table: "BonSorties");

            migrationBuilder.AddForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdClient",
                table: "BonSorties",
                column: "IdClient",
                principalTable: "Fournisseurs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonSorties_Fournisseurs_IdClient",
                table: "BonSorties");

            migrationBuilder.AddForeignKey(
                name: "FK_BonSorties_Clients_IdClient",
                table: "BonSorties",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
