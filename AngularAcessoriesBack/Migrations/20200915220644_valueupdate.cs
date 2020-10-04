using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularAcessoriesBack.Migrations
{
    public partial class valueupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryValues_Categories_Category",
                table: "CategoryValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFilters_Categories_Category",
                table: "ProductFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiltersValues_CategoryValues_Value",
                table: "ProductFiltersValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiltersValues_ProductFilters_productId",
                table: "ProductFiltersValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiltersValues_Value",
                table: "ProductFiltersValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiltersValues_productId",
                table: "ProductFiltersValues");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductFilters_productId",
                table: "ProductFilters");

            migrationBuilder.DropIndex(
                name: "IX_ProductFilters_Category",
                table: "ProductFilters");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CategoryValues_Value",
                table: "CategoryValues");

            migrationBuilder.DropIndex(
                name: "IX_CategoryValues_Category",
                table: "CategoryValues");

            migrationBuilder.DropIndex(
                name: "IX_CategoryValues_Value",
                table: "CategoryValues");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Category",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Category",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductFiltersValues",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "categoryValuesId",
                table: "ProductFiltersValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productFiltersId",
                table: "ProductFiltersValues",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ProductFilters",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "ProductFilters",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CategoryValues",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "CategoryValues",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "CategoryValues",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "categories",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_categoryValuesId",
                table: "ProductFiltersValues",
                column: "categoryValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_productFiltersId",
                table: "ProductFiltersValues",
                column: "productFiltersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_categoryId",
                table: "ProductFilters",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_productId",
                table: "ProductFilters",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryValues_categoryId",
                table: "CategoryValues",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryValues_categories_categoryId",
                table: "CategoryValues",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFilters_categories_categoryId",
                table: "ProductFilters",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiltersValues_CategoryValues_categoryValuesId",
                table: "ProductFiltersValues",
                column: "categoryValuesId",
                principalTable: "CategoryValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiltersValues_ProductFilters_productFiltersId",
                table: "ProductFiltersValues",
                column: "productFiltersId",
                principalTable: "ProductFilters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryValues_categories_categoryId",
                table: "CategoryValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFilters_categories_categoryId",
                table: "ProductFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiltersValues_CategoryValues_categoryValuesId",
                table: "ProductFiltersValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiltersValues_ProductFilters_productFiltersId",
                table: "ProductFiltersValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiltersValues_categoryValuesId",
                table: "ProductFiltersValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFiltersValues_productFiltersId",
                table: "ProductFiltersValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFilters_categoryId",
                table: "ProductFilters");

            migrationBuilder.DropIndex(
                name: "IX_ProductFilters_productId",
                table: "ProductFilters");

            migrationBuilder.DropIndex(
                name: "IX_CategoryValues_categoryId",
                table: "CategoryValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "categoryValuesId",
                table: "ProductFiltersValues");

            migrationBuilder.DropColumn(
                name: "productFiltersId",
                table: "ProductFiltersValues");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "ProductFilters");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "CategoryValues");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProductFiltersValues",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ProductFilters",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CategoryValues",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "CategoryValues",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductFilters_productId",
                table: "ProductFilters",
                column: "productId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CategoryValues_Value",
                table: "CategoryValues",
                column: "Value");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Category",
                table: "Categories",
                column: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_Value",
                table: "ProductFiltersValues",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFiltersValues_productId",
                table: "ProductFiltersValues",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFilters_Category",
                table: "ProductFilters",
                column: "Category");

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
                name: "IX_Categories_Category",
                table: "Categories",
                column: "Category",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryValues_Categories_Category",
                table: "CategoryValues",
                column: "Category",
                principalTable: "Categories",
                principalColumn: "Category",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFilters_Categories_Category",
                table: "ProductFilters",
                column: "Category",
                principalTable: "Categories",
                principalColumn: "Category",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiltersValues_CategoryValues_Value",
                table: "ProductFiltersValues",
                column: "Value",
                principalTable: "CategoryValues",
                principalColumn: "Value",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiltersValues_ProductFilters_productId",
                table: "ProductFiltersValues",
                column: "productId",
                principalTable: "ProductFilters",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
