using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class AddedMemberCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberCategory",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.AddColumn<int>(
                name: "MemberCategoryId",
                schema: "Team",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeamMemberCategories",
                schema: "Team",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamMemberCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMemberCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_MemberCategoryId",
                schema: "Team",
                table: "TeamMembers",
                column: "MemberCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_TeamMemberCategories_MemberCategoryId",
                schema: "Team",
                table: "TeamMembers",
                column: "MemberCategoryId",
                principalSchema: "Team",
                principalTable: "TeamMemberCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_TeamMemberCategories_MemberCategoryId",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TeamMemberCategories",
                schema: "Team");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_MemberCategoryId",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "MemberCategoryId",
                schema: "Team",
                table: "TeamMembers");

            migrationBuilder.AddColumn<string>(
                name: "MemberCategory",
                schema: "Team",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
