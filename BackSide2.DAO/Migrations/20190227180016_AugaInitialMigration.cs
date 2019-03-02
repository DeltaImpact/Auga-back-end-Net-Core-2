using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auga.DAO.Migrations
{
    public partial class AugaInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_PersonId",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "BoardPin");

            migrationBuilder.DropTable(
                name: "ChatConnectedUser");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Pins");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Boards",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_PersonId",
                table: "Boards",
                newName: "IX_Boards_SellerId");

            migrationBuilder.AddColumn<long>(
                name: "BuyerId",
                table: "Boards",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Boards",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ChatConnectedUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConnectedUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BuyerId",
                table: "Boards",
                column: "BuyerId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_BuyerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Persons_SellerId",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "ChatConnectedUsers");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BuyerId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Boards",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_SellerId",
                table: "Boards",
                newName: "IX_Boards_PersonId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Language",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Persons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Boards",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Boards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ChatConnectedUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConnectionId = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConnectedUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    Received = table.Column<bool>(nullable: false),
                    ReceivedById = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Persons_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pins",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardPin",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoardId = table.Column<long>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    PinId = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardPin_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardPin_Pins_PinId",
                        column: x => x.PinId,
                        principalTable: "Pins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardPin_BoardId",
                table: "BoardPin",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardPin_PinId",
                table: "BoardPin",
                column: "PinId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceivedById",
                table: "ChatMessages",
                column: "ReceivedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Persons_PersonId",
                table: "Boards",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
