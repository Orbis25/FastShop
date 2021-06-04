using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class AdditionalFieldFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_AditionalFields_AditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetail_AditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "AditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_AdditionalFieldId",
                table: "ProductDetail",
                column: "AdditionalFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_AditionalFields_AdditionalFieldId",
                table: "ProductDetail",
                column: "AdditionalFieldId",
                principalTable: "AditionalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_AditionalFields_AdditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetail_AdditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.AddColumn<int>(
                name: "AditionalFieldId",
                table: "ProductDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_AditionalFieldId",
                table: "ProductDetail",
                column: "AditionalFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_AditionalFields_AditionalFieldId",
                table: "ProductDetail",
                column: "AditionalFieldId",
                principalTable: "AditionalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
