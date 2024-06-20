using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDepot.Migrations
{
    /// <inheritdoc />
    public partial class migoss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroBon",
                table: "JournalStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroBon",
                table: "JournalStock");
        }
    }
}
