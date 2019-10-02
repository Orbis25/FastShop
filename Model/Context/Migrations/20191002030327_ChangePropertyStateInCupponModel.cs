using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class ChangePropertyStateInCupponModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ImageOfferts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StateOfCuppon",
                table: "Cupons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateOfCuppon",
                table: "Cupons");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ImageOfferts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
