using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class addingLazyLoading2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Sales",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Sales");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
