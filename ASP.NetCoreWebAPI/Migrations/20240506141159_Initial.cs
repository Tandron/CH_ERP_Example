using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPurchases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillFromPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostalCharges = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CompanyPurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillFromPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillFromPurchases_CompanyPurchases_CompanyPurchaseId",
                        column: x => x.CompanyPurchaseId,
                        principalTable: "CompanyPurchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Metals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetalType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillFromPurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metals_BillFromPurchases_BillFromPurchaseId",
                        column: x => x.BillFromPurchaseId,
                        principalTable: "BillFromPurchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plastics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaticType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillFromPurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plastics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plastics_BillFromPurchases_BillFromPurchaseId",
                        column: x => x.BillFromPurchaseId,
                        principalTable: "BillFromPurchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillFromPurchases_CompanyPurchaseId",
                table: "BillFromPurchases",
                column: "CompanyPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Metals_BillFromPurchaseId",
                table: "Metals",
                column: "BillFromPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Plastics_BillFromPurchaseId",
                table: "Plastics",
                column: "BillFromPurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metals");

            migrationBuilder.DropTable(
                name: "Plastics");

            migrationBuilder.DropTable(
                name: "BillFromPurchases");

            migrationBuilder.DropTable(
                name: "CompanyPurchases");
        }
    }
}
