using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class RemovedIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Team",
                table: "TeamMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Team",
                table: "TeamMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
