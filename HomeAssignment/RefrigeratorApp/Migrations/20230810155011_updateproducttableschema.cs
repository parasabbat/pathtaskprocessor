using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefrigeratorApp.Migrations
{
    /// <inheritdoc />
    public partial class updateproducttableschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLogs");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductInventoryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInventoryLogs");

            migrationBuilder.AddColumn<double>(
                name: "CurrentQuantity",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ProductLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quanity = table.Column<double>(type: "REAL", nullable: false),
                    QuantityUnit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLogs", x => x.Id);
                });
        }
    }
}
