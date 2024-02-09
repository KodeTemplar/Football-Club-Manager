using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TeamMemberImages_Teams_TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages");

            //migrationBuilder.DropIndex(
            //    name: "IX_TeamMemberImages_TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages");

            //migrationBuilder.DropColumn(
            //    name: "TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<Guid>(
            //    name: "TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateIndex(
            //    name: "IX_TeamMemberImages_TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages",
            //    column: "TeamId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TeamMemberImages_Teams_TeamId",
            //    schema: "Team",
            //    table: "TeamMemberImages",
            //    column: "TeamId",
            //    principalSchema: "Team",
            //    principalTable: "Teams",
            //    principalColumn: "TeamId",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
