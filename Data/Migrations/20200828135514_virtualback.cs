using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAanbod.Data.Migrations
{
    public partial class virtualback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_LaatstAangepast_LaatstAangepastId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "LaatstAangepast");

            migrationBuilder.DropIndex(
                name: "IX_Product_LaatstAangepastId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LaatstAangepastId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GewijzigdDatum",
                table: "Product",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "GewijzigdDatum",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "LaatstAangepastId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LaatstAangepast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GewijzigdDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaatstAangepast", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_LaatstAangepastId",
                table: "Product",
                column: "LaatstAangepastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_LaatstAangepast_LaatstAangepastId",
                table: "Product",
                column: "LaatstAangepastId",
                principalTable: "LaatstAangepast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
