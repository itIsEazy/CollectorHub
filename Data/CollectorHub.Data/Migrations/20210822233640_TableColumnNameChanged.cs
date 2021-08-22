using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class TableColumnNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotWheelsCars_HotWheelsTypes_TypeId",
                table: "HotWheelsCars");

            migrationBuilder.DropForeignKey(
                name: "FK_HotWheelsCollections_HotWheelsTypes_TypeId",
                table: "HotWheelsCollections");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "HotWheelsCollections",
                newName: "HotWheelsTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotWheelsCollections_TypeId",
                table: "HotWheelsCollections",
                newName: "IX_HotWheelsCollections_HotWheelsTypeId");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "HotWheelsCars",
                newName: "HotWheelsTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotWheelsCars_TypeId",
                table: "HotWheelsCars",
                newName: "IX_HotWheelsCars_HotWheelsTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotWheelsCars_HotWheelsTypes_HotWheelsTypeId",
                table: "HotWheelsCars",
                column: "HotWheelsTypeId",
                principalTable: "HotWheelsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotWheelsCollections_HotWheelsTypes_HotWheelsTypeId",
                table: "HotWheelsCollections",
                column: "HotWheelsTypeId",
                principalTable: "HotWheelsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotWheelsCars_HotWheelsTypes_HotWheelsTypeId",
                table: "HotWheelsCars");

            migrationBuilder.DropForeignKey(
                name: "FK_HotWheelsCollections_HotWheelsTypes_HotWheelsTypeId",
                table: "HotWheelsCollections");

            migrationBuilder.RenameColumn(
                name: "HotWheelsTypeId",
                table: "HotWheelsCollections",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotWheelsCollections_HotWheelsTypeId",
                table: "HotWheelsCollections",
                newName: "IX_HotWheelsCollections_TypeId");

            migrationBuilder.RenameColumn(
                name: "HotWheelsTypeId",
                table: "HotWheelsCars",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotWheelsCars_HotWheelsTypeId",
                table: "HotWheelsCars",
                newName: "IX_HotWheelsCars_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotWheelsCars_HotWheelsTypes_TypeId",
                table: "HotWheelsCars",
                column: "TypeId",
                principalTable: "HotWheelsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotWheelsCollections_HotWheelsTypes_TypeId",
                table: "HotWheelsCollections",
                column: "TypeId",
                principalTable: "HotWheelsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
