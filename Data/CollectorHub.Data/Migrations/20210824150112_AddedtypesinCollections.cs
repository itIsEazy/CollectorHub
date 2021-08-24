using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedtypesinCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectionTypeId",
                table: "LegoCollections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegoTypeId",
                table: "LegoCollections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectionTypeId",
                table: "FastAndFuriousPremiumCollections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollections_CollectionTypeId",
                table: "LegoCollections",
                column: "CollectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollections_LegoTypeId",
                table: "LegoCollections",
                column: "LegoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FastAndFuriousPremiumCollections_CollectionTypeId",
                table: "FastAndFuriousPremiumCollections",
                column: "CollectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_CollectionTypes_CollectionTypeId",
                table: "FastAndFuriousPremiumCollections",
                column: "CollectionTypeId",
                principalTable: "CollectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegoCollections_CollectionTypes_CollectionTypeId",
                table: "LegoCollections",
                column: "CollectionTypeId",
                principalTable: "CollectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegoCollections_LegoTypes_LegoTypeId",
                table: "LegoCollections",
                column: "LegoTypeId",
                principalTable: "LegoTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_CollectionTypes_CollectionTypeId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_LegoCollections_CollectionTypes_CollectionTypeId",
                table: "LegoCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_LegoCollections_LegoTypes_LegoTypeId",
                table: "LegoCollections");

            migrationBuilder.DropIndex(
                name: "IX_LegoCollections_CollectionTypeId",
                table: "LegoCollections");

            migrationBuilder.DropIndex(
                name: "IX_LegoCollections_LegoTypeId",
                table: "LegoCollections");

            migrationBuilder.DropIndex(
                name: "IX_FastAndFuriousPremiumCollections_CollectionTypeId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropColumn(
                name: "CollectionTypeId",
                table: "LegoCollections");

            migrationBuilder.DropColumn(
                name: "LegoTypeId",
                table: "LegoCollections");

            migrationBuilder.DropColumn(
                name: "CollectionTypeId",
                table: "FastAndFuriousPremiumCollections");
        }
    }
}
