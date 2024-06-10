using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSociete",
                table: "Fournisseurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdSociete",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseurs_IdSociete",
                table: "Fournisseurs",
                column: "IdSociete");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IdSociete",
                table: "Clients",
                column: "IdSociete");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Societes_IdSociete",
                table: "Clients",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fournisseurs_Societes_IdSociete",
                table: "Fournisseurs",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Societes_IdSociete",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Fournisseurs_Societes_IdSociete",
                table: "Fournisseurs");

            migrationBuilder.DropIndex(
                name: "IX_Fournisseurs_IdSociete",
                table: "Fournisseurs");

            migrationBuilder.DropIndex(
                name: "IX_Clients_IdSociete",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IdSociete",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "IdSociete",
                table: "Clients");
        }
    }
}
