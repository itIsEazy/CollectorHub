using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedStringIdsWhereMissing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionId",
                table: "FastAndFuriousPremiumItem");

            migrationBuilder.RenameColumn(
                name: "FastAndFuriousPremiumCollectionId",
                table: "FastAndFuriousPremiumItem",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollectionId",
                table: "FastAndFuriousPremiumItem",
                newName: "IX_FastAndFuriousPremiumItem_CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollections_CollectionId",
                table: "FastAndFuriousPremiumItem",
                column: "CollectionId",
                principalTable: "FastAndFuriousPremiumCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollections_CollectionId",
                table: "FastAndFuriousPremiumItem");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "FastAndFuriousPremiumItem",
                newName: "FastAndFuriousPremiumCollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_FastAndFuriousPremiumItem_CollectionId",
                table: "FastAndFuriousPremiumItem",
                newName: "IX_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumItem_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionId",
                table: "FastAndFuriousPremiumItem",
                column: "FastAndFuriousPremiumCollectionId",
                principalTable: "FastAndFuriousPremiumCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
