using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class addingDetailSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailSale_Products_ProductId",
                table: "DetailSale");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailSale_Sales_SaleId",
                table: "DetailSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailSale",
                table: "DetailSale");

            migrationBuilder.RenameTable(
                name: "DetailSale",
                newName: "DetailSales");

            migrationBuilder.RenameIndex(
                name: "IX_DetailSale_SaleId",
                table: "DetailSales",
                newName: "IX_DetailSales_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailSale_ProductId",
                table: "DetailSales",
                newName: "IX_DetailSales_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailSales",
                table: "DetailSales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailSales_Products_ProductId",
                table: "DetailSales",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailSales_Sales_SaleId",
                table: "DetailSales",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailSales_Products_ProductId",
                table: "DetailSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailSales_Sales_SaleId",
                table: "DetailSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailSales",
                table: "DetailSales");

            migrationBuilder.RenameTable(
                name: "DetailSales",
                newName: "DetailSale");

            migrationBuilder.RenameIndex(
                name: "IX_DetailSales_SaleId",
                table: "DetailSale",
                newName: "IX_DetailSale_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailSales_ProductId",
                table: "DetailSale",
                newName: "IX_DetailSale_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailSale",
                table: "DetailSale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailSale_Products_ProductId",
                table: "DetailSale",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailSale_Sales_SaleId",
                table: "DetailSale",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
