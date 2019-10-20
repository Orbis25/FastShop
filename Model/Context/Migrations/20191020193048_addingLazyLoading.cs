using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class addingLazyLoading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId1",
                table: "Sales",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId1",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Sales");
        }
    }
}
