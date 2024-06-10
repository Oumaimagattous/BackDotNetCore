using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration2112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Societes_IdSociete",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Fournisseurs_Societes_IdSociete",
                table: "Fournisseurs");

            migrationBuilder.AlterColumn<int>(
                name: "IdSociete",
                table: "Fournisseurs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdSociete",
                table: "Clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Societes_IdSociete",
                table: "Clients",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fournisseurs_Societes_IdSociete",
                table: "Fournisseurs",
                column: "IdSociete",
                principalTable: "Societes",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "IdSociete",
                table: "Fournisseurs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdSociete",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
