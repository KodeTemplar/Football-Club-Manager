using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class RelationshipImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamMemberImages_TeamMemberId",
                schema: "Team",
                table: "TeamMemberImages");

            migrationBuilder.DropIndex(
                name: "IX_TeamLogos_TeamId",
                schema: "Team",
                table: "TeamLogos");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberImages_TeamMemberId",
                schema: "Team",
                table: "TeamMemberImages",
                column: "TeamMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamLogos_TeamId",
                schema: "Team",
                table: "TeamLogos",
                column: "TeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamMemberImages_TeamMemberId",
                schema: "Team",
                table: "TeamMemberImages");

            migrationBuilder.DropIndex(
                name: "IX_TeamLogos_TeamId",
                schema: "Team",
                table: "TeamLogos");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberImages_TeamMemberId",
                schema: "Team",
                table: "TeamMemberImages",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLogos_TeamId",
                schema: "Team",
                table: "TeamLogos",
                column: "TeamId");
        }
    }
}
