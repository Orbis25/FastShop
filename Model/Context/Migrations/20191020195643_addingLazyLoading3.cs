using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class addingLazyLoading3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ApplicationUserId",
                table: "Sales",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_ApplicationUserId",
                table: "Sales",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_ApplicationUserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ApplicationUserId",
                table: "Sales");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
