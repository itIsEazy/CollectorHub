using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedStringIdsWhereMissing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_Categories_CategoryId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_SubCategories_SubcategoryId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_SubcategoryId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_FastAndFuriousPremiumCollections_CategoryId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.AlterColumn<string>(
                name: "SubcategoryId",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubcategoryId1",
                table: "SubCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "FastAndFuriousPremiumCollections",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "FastAndFuriousPremiumCollections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubcategoryId1",
                table: "SubCategories",
                column: "SubcategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_FastAndFuriousPremiumCollections_CategoryId1",
                table: "FastAndFuriousPremiumCollections",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_Categories_CategoryId1",
                table: "FastAndFuriousPremiumCollections",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_SubCategories_SubcategoryId1",
                table: "SubCategories",
                column: "SubcategoryId1",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_Categories_CategoryId1",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_SubCategories_SubcategoryId1",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_SubcategoryId1",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_FastAndFuriousPremiumCollections_CategoryId1",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropColumn(
                name: "SubcategoryId1",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.AlterColumn<string>(
                name: "SubcategoryId",
                table: "SubCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "FastAndFuriousPremiumCollections",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubcategoryId",
                table: "SubCategories",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FastAndFuriousPremiumCollections_CategoryId",
                table: "FastAndFuriousPremiumCollections",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_Categories_CategoryId",
                table: "FastAndFuriousPremiumCollections",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_SubCategories_SubcategoryId",
                table: "SubCategories",
                column: "SubcategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
