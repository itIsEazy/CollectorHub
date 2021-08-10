using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedCollectionShowPricesProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowPrices",
                table: "FastAndFuriousPremiumCollections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowPrices",
                table: "FastAndFuriousPremiumCollections");
        }
    }
}
