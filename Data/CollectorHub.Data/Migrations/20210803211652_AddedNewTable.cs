using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollectorHub.Data.Migrations
{
    public partial class AddedNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumPosts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumPostComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FastAndFuriousPremiumCollectionApplicationUserId",
                table: "FastAndFuriousPremiumCollections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                table: "FastAndFuriousPremiumCollections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FastAndFuriousPremiumCollectionsApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastAndFuriousPremiumCollectionsApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SubcategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_SubCategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "FastAndFuriousPremiumCollections",
                column: "FastAndFuriousPremiumCollectionApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubCategoryId",
                table: "Categories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers",
                column: "FastAndFuriousPremiumCollectionApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_IsDeleted",
                table: "SubCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubcategoryId",
                table: "SubCategories",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FastAndFuriousPremiumCollectionsApplicationUsers_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers",
                column: "FastAndFuriousPremiumCollectionApplicationUserId",
                principalTable: "FastAndFuriousPremiumCollectionsApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryId",
                table: "Categories",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionsApplicationUsers_FastAndFuriousPremiumCollectionApplication~",
                table: "FastAndFuriousPremiumCollections",
                column: "FastAndFuriousPremiumCollectionApplicationUserId",
                principalTable: "FastAndFuriousPremiumCollectionsApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FastAndFuriousPremiumCollectionsApplicationUsers_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionsApplicationUsers_FastAndFuriousPremiumCollectionApplication~",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropTable(
                name: "FastAndFuriousPremiumCollectionsApplicationUsers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_FastAndFuriousPremiumCollections_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SubCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FastAndFuriousPremiumCollectionApplicationUserId",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropColumn(
                name: "ViewsCount",
                table: "FastAndFuriousPremiumCollections");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FastAndFuriousPremiumCollectionApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumPosts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "ForumPostComments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
