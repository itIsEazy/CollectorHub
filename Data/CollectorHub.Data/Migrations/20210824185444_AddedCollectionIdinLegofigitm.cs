using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedCollectionIdinLegofigitm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegoCollectionLegoMinifigureItem");

            migrationBuilder.AddColumn<string>(
                name: "CollectionId",
                table: "LegoMinifigureItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LegoMinifigureItems_CollectionId",
                table: "LegoMinifigureItems",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegoMinifigureItems_LegoCollections_CollectionId",
                table: "LegoMinifigureItems",
                column: "CollectionId",
                principalTable: "LegoCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegoMinifigureItems_LegoCollections_CollectionId",
                table: "LegoMinifigureItems");

            migrationBuilder.DropIndex(
                name: "IX_LegoMinifigureItems_CollectionId",
                table: "LegoMinifigureItems");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "LegoMinifigureItems");

            migrationBuilder.CreateTable(
                name: "LegoCollectionLegoMinifigureItem",
                columns: table => new
                {
                    CollectionsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegoCollectionLegoMinifigureItem", x => new { x.CollectionsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_LegoCollectionLegoMinifigureItem_LegoCollections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "LegoCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegoCollectionLegoMinifigureItem_LegoMinifigureItems_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "LegoMinifigureItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollectionLegoMinifigureItem_ItemsId",
                table: "LegoCollectionLegoMinifigureItem",
                column: "ItemsId");
        }
    }
}
