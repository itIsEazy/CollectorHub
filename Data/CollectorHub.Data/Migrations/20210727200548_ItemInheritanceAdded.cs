using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class ItemInheritanceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCollection_CollectionsId",
                table: "PremiumHWCarPremiumHWCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_PremiumHWCollection_AspNetUsers_UserId",
                table: "PremiumHWCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PremiumHWCollection",
                table: "PremiumHWCollection");

            migrationBuilder.RenameTable(
                name: "PremiumHWCollection",
                newName: "PremiumHWCollections");

            migrationBuilder.RenameIndex(
                name: "IX_PremiumHWCollection_UserId",
                table: "PremiumHWCollections",
                newName: "IX_PremiumHWCollections_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PremiumHWCollection_IsDeleted",
                table: "PremiumHWCollections",
                newName: "IX_PremiumHWCollections_IsDeleted");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBoughted",
                table: "PremiumHWCars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceNow",
                table: "PremiumHWCars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PremiumHWCollections",
                table: "PremiumHWCollections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCollections_CollectionsId",
                table: "PremiumHWCarPremiumHWCollection",
                column: "CollectionsId",
                principalTable: "PremiumHWCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PremiumHWCollections_AspNetUsers_UserId",
                table: "PremiumHWCollections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCollections_CollectionsId",
                table: "PremiumHWCarPremiumHWCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_PremiumHWCollections_AspNetUsers_UserId",
                table: "PremiumHWCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PremiumHWCollections",
                table: "PremiumHWCollections");

            migrationBuilder.DropColumn(
                name: "PriceBoughted",
                table: "PremiumHWCars");

            migrationBuilder.DropColumn(
                name: "PriceNow",
                table: "PremiumHWCars");

            migrationBuilder.RenameTable(
                name: "PremiumHWCollections",
                newName: "PremiumHWCollection");

            migrationBuilder.RenameIndex(
                name: "IX_PremiumHWCollections_UserId",
                table: "PremiumHWCollection",
                newName: "IX_PremiumHWCollection_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PremiumHWCollections_IsDeleted",
                table: "PremiumHWCollection",
                newName: "IX_PremiumHWCollection_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PremiumHWCollection",
                table: "PremiumHWCollection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCollection_CollectionsId",
                table: "PremiumHWCarPremiumHWCollection",
                column: "CollectionsId",
                principalTable: "PremiumHWCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PremiumHWCollection_AspNetUsers_UserId",
                table: "PremiumHWCollection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
