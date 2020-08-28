using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAanbod.Data.Migrations
{
    public partial class StartData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Catogorie_CatogorieId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CatogorieId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CatogorieId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategorieId",
                table: "Product",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Catogorie_CategorieId",
                table: "Product",
                column: "CategorieId",
                principalTable: "Catogorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Catogorie_CategorieId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategorieId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "CatogorieId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatogorieId",
                table: "Product",
                column: "CatogorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Catogorie_CatogorieId",
                table: "Product",
                column: "CatogorieId",
                principalTable: "Catogorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
