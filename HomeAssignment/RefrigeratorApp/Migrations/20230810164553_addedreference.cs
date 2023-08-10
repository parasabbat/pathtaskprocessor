using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefrigeratorApp.Migrations
{
    /// <inheritdoc />
    public partial class addedreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryLogs_ProductId",
                table: "ProductInventoryLogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryLogs_Products_ProductId",
                table: "ProductInventoryLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryLogs_Products_ProductId",
                table: "ProductInventoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventoryLogs_ProductId",
                table: "ProductInventoryLogs");
        }
    }
}
