using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migf3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalCasiers_BonEntrees_IdBonEntree",
                table: "JournalCasiers");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_BonEntrees_IdBonEntree",
                table: "JournalStock");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalCasiers_BonEntrees_IdBonEntree",
                table: "JournalCasiers",
                column: "IdBonEntree",
                principalTable: "BonEntrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_BonEntrees_IdBonEntree",
                table: "JournalStock",
                column: "IdBonEntree",
                principalTable: "BonEntrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalCasiers_BonEntrees_IdBonEntree",
                table: "JournalCasiers");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalStock_BonEntrees_IdBonEntree",
                table: "JournalStock");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalCasiers_BonEntrees_IdBonEntree",
                table: "JournalCasiers",
                column: "IdBonEntree",
                principalTable: "BonEntrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalStock_BonEntrees_IdBonEntree",
                table: "JournalStock",
                column: "IdBonEntree",
                principalTable: "BonEntrees",
                principalColumn: "Id");
        }
    }
}
