using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularAcessoriesBack.Migrations
{
    public partial class changecart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserCart",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "UserCart",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "UnitePrice",
                table: "UserCart",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserCart");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "UserCart");

            migrationBuilder.DropColumn(
                name: "UnitePrice",
                table: "UserCart");
        }
    }
}
