using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class AddPathToProductPic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ProductPics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "ProductPics");
        }
    }
}
