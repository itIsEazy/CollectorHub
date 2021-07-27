using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class HotWheelsCollectionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PremiumHWCollection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumHWCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PremiumHWCollection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PremiumHWCarPremiumHWCollection",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    CollectionsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumHWCarPremiumHWCollection", x => new { x.CarsId, x.CollectionsId });
                    table.ForeignKey(
                        name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "PremiumHWCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PremiumHWCarPremiumHWCollection_PremiumHWCollection_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "PremiumHWCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PremiumHWCarPremiumHWCollection_CollectionsId",
                table: "PremiumHWCarPremiumHWCollection",
                column: "CollectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumHWCollection_IsDeleted",
                table: "PremiumHWCollection",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumHWCollection_UserId",
                table: "PremiumHWCollection",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PremiumHWCarPremiumHWCollection");

            migrationBuilder.DropTable(
                name: "PremiumHWCollection");
        }
    }
}
