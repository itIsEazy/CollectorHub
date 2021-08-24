using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedLegoTypesStarWarsOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LegoTypeId",
                table: "LegoMinifigures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LegoTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegoTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegoMinifigures_LegoTypeId",
                table: "LegoMinifigures",
                column: "LegoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LegoTypes_IsDeleted",
                table: "LegoTypes",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_LegoMinifigures_LegoTypes_LegoTypeId",
                table: "LegoMinifigures",
                column: "LegoTypeId",
                principalTable: "LegoTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegoMinifigures_LegoTypes_LegoTypeId",
                table: "LegoMinifigures");

            migrationBuilder.DropTable(
                name: "LegoTypes");

            migrationBuilder.DropIndex(
                name: "IX_LegoMinifigures_LegoTypeId",
                table: "LegoMinifigures");

            migrationBuilder.DropColumn(
                name: "LegoTypeId",
                table: "LegoMinifigures");
        }
    }
}
