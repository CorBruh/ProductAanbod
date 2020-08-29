using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAanbod.Data.Migrations
{
    public partial class premiepermaand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Premie",
                table: "Product",
                newName: "PremiePerMaand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PremiePerMaand",
                table: "Product",
                newName: "Premie");
        }
    }
}
