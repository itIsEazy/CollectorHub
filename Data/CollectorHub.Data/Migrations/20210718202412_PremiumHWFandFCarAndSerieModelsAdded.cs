using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class PremiumHWFandFCarAndSerieModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegoItems_Collections_CollectionId",
                table: "LegoItems");

            migrationBuilder.DropIndex(
                name: "IX_LegoItems_CollectionId",
                table: "LegoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "LegoCollections");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_IsDeleted",
                table: "LegoCollections",
                newName: "IX_LegoCollections_IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LegoCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LegoCollections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LegoCollections",
                table: "LegoCollections",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LegoCollectionLegoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegoCollectionLegoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegoCollectionLegoItem_LegoCollections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "LegoCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegoCollectionLegoItem_LegoItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "LegoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PremiumHWSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderOfApperance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumHWSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PremiumHWCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Col = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tampos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WheelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Movie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoLooseLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoCardLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumHWCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PremiumHWCars_PremiumHWSeries_SerieId",
                        column: x => x.SerieId,
                        principalTable: "PremiumHWSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollections_UserId",
                table: "LegoCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollectionLegoItem_CollectionId",
                table: "LegoCollectionLegoItem",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LegoCollectionLegoItem_ItemId",
                table: "LegoCollectionLegoItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumHWCars_SerieId",
                table: "PremiumHWCars",
                column: "SerieId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegoCollections_AspNetUsers_UserId",
                table: "LegoCollections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegoCollections_AspNetUsers_UserId",
                table: "LegoCollections");

            migrationBuilder.DropTable(
                name: "LegoCollectionLegoItem");

            migrationBuilder.DropTable(
                name: "PremiumHWCars");

            migrationBuilder.DropTable(
                name: "PremiumHWSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LegoCollections",
                table: "LegoCollections");

            migrationBuilder.DropIndex(
                name: "IX_LegoCollections_UserId",
                table: "LegoCollections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LegoCollections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LegoCollections");

            migrationBuilder.RenameTable(
                name: "LegoCollections",
                newName: "Collections");

            migrationBuilder.RenameIndex(
                name: "IX_LegoCollections_IsDeleted",
                table: "Collections",
                newName: "IX_Collections_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LegoItems_CollectionId",
                table: "LegoItems",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LegoItems_Collections_CollectionId",
                table: "LegoItems",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
