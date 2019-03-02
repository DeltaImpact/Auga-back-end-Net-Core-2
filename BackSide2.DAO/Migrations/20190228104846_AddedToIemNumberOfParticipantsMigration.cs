using Microsoft.EntityFrameworkCore.Migrations;

namespace Auga.DAO.Migrations
{
    public partial class AddedToIemNumberOfParticipantsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NumberOfParticipants",
                table: "Items",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "Items");
        }
    }
}
