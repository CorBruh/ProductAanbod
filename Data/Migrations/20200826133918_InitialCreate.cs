using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAanbod.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catogorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieNaam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catogorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaatstAangepast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    GewijzigdDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaatstAangepast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verzekeraar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerzekeraarNaam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verzekeraar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductNaam = table.Column<string>(nullable: false),
                    Premie = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Actief = table.Column<bool>(nullable: false),
                    CatogorieId = table.Column<int>(nullable: true),
                    LaatstAangepastId = table.Column<int>(nullable: true),
                    VerzekeraarId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Catogorie_CatogorieId",
                        column: x => x.CatogorieId,
                        principalTable: "Catogorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_LaatstAangepast_LaatstAangepastId",
                        column: x => x.LaatstAangepastId,
                        principalTable: "LaatstAangepast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Verzekeraar_VerzekeraarId",
                        column: x => x.VerzekeraarId,
                        principalTable: "Verzekeraar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatogorieId",
                table: "Product",
                column: "CatogorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LaatstAangepastId",
                table: "Product",
                column: "LaatstAangepastId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VerzekeraarId",
                table: "Product",
                column: "VerzekeraarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Catogorie");

            migrationBuilder.DropTable(
                name: "LaatstAangepast");

            migrationBuilder.DropTable(
                name: "Verzekeraar");
        }
    }
}
