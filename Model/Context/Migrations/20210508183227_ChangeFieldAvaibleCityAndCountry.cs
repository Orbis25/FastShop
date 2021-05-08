using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Data.Migrations
{
    public partial class ChangeFieldAvaibleCityAndCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AvaibleCities");

            migrationBuilder.AlterColumn<string>(
                name: "Iso3",
                table: "AvaibleCountries",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<long>(
                name: "Lat",
                table: "AvaibleCities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Long",
                table: "AvaibleCities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "AvaibleCities");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "AvaibleCities");

            migrationBuilder.AlterColumn<string>(
                name: "Iso3",
                table: "AvaibleCountries",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AvaibleCities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
