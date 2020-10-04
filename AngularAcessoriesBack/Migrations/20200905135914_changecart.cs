using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularAcessoriesBack.Migrations
{
    public partial class changecart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "UserCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "UserCart",
                nullable: false,
                defaultValue: "");
        }
    }
}
