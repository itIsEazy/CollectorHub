using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedCategoryToCollectionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FastAndFuriousPremiumCollections",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_Categories_CategoryId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropIndex(
                name: "IX_FastAndFuriousPremiumCollections_CategoryId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FastAndFuriousPremiumCollections");
        }
    }
}
