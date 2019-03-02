using Microsoft.EntityFrameworkCore.Migrations;

namespace Auga.DAO.Migrations
{
    public partial class InitialAugaNamingFixForDbSetsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_BuyerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_SellerId",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_SellerId",
                table: "Items",
                newName: "IX_Items_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_BuyerId",
                table: "Items",
                newName: "IX_Items_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_BuyerId",
                table: "Items",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_SellerId",
                table: "Items",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_BuyerId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_SellerId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_Items_SellerId",
                table: "Boards",
                newName: "IX_Boards_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_BuyerId",
                table: "Boards",
                newName: "IX_Boards_BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Persons_BuyerId",
                table: "Boards",
                column: "BuyerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Persons_SellerId",
                table: "Boards",
                column: "SellerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
