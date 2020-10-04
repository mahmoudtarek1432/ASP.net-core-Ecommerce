using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularAcessoriesBack.Migrations
{
    public partial class UpdateProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FeaturedItem",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnDisplay",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnDisplay",
                table: "Banners",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedItem",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OnDisplay",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OnDisplay",
                table: "Banners");
        }
    }
}
