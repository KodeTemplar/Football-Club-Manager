using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class ModifiedMemberCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberType",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.AddColumn<string>(
                name: "MemberCategory",
                schema: "Team",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberImages_Teams_TeamId",
                schema: "Team",
                table: "TeamMemberImages");

            migrationBuilder.DropIndex(
                name: "IX_TeamMemberImages_TeamId",
                schema: "Team",
                table: "TeamMemberImages");

            migrationBuilder.DropColumn(
                name: "MemberCategory",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                schema: "Team",
                table: "TeamMemberImages");

            migrationBuilder.AddColumn<int>(
                name: "MemberType",
                schema: "Team",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
