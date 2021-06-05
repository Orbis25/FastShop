using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class ReingeneerProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetail_AditionalFields_AdditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.DropTable(
                name: "AditionalFields");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetail_AdditionalFieldId",
                table: "ProductDetail");

            migrationBuilder.RenameColumn(
                name: "AdditionalFieldId",
                table: "ProductDetail",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductDetail");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ProductDetail",
                newName: "AdditionalFieldId");

            migrationBuilder.CreateTable(
                name: "AditionalFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selectionable = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AditionalFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AditionalFields_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_AdditionalFieldId",
                table: "ProductDetail",
                column: "AdditionalFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_AditionalFields_CategoryId",
                table: "AditionalFields",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetail_AditionalFields_AdditionalFieldId",
                table: "ProductDetail",
                column: "AdditionalFieldId",
                principalTable: "AditionalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
