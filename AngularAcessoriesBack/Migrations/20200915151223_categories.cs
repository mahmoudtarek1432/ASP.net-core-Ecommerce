using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularAcessoriesBack.Migrations
{
    public partial class categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Category", x => x.Category);
                });

            migrationBuilder.CreateTable(
                name: "CategoryValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryValues", x => x.Id);
                    table.UniqueConstraint("AK_CategoryValues_Value", x => x.Value);
                    table.ForeignKey(
                        name: "FK_CategoryValues_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "Category",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    productId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFilters", x => x.Id);
                    table.UniqueConstraint("AK_ProductFilters_productId", x => x.productId);
                    table.ForeignKey(
                        name: "FK_ProductFilters_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "Category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFilters_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFiltersValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFiltersValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFiltersValues_CategoryValues_Value",
                        column: x => x.Value,
                        principalTable: "CategoryValues",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFiltersValues_ProductFilters_productId",
                        column: x => x.productId,
                        principalTable: "ProductFilters",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Category",
                table: "Categories",
                column: "Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryValues_Category",
                table: "CategoryValues",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryValues_Value",
                table: "CategoryValues",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_Category",
                table: "ProductFilters",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_Value",
                table: "ProductFiltersValues",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_productId",
                table: "ProductFiltersValues",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFiltersValues");

            migrationBuilder.DropTable(
                name: "CategoryValues");

            migrationBuilder.DropTable(
                name: "ProductFilters");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
