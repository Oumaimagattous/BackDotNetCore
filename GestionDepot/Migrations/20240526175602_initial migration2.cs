using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Societe",
                table: "Societe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factures",
                table: "Factures");

            migrationBuilder.RenameTable(
                name: "Societe",
                newName: "Societes");

            migrationBuilder.RenameTable(
                name: "Factures",
                newName: "Fournisseurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Societes",
                table: "Societes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fournisseurs",
                table: "Fournisseurs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Societes",
                table: "Societes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fournisseurs",
                table: "Fournisseurs");

            migrationBuilder.RenameTable(
                name: "Societes",
                newName: "Societe");

            migrationBuilder.RenameTable(
                name: "Fournisseurs",
                newName: "Factures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Societe",
                table: "Societe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factures",
                table: "Factures",
                column: "Id");
        }
    }
}
