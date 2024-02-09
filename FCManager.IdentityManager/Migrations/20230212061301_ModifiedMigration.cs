using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.IdentityManager.Migrations
{
    public partial class ModifiedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_TeamMembers_TeamMemberId",
                schema: "Identity",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamMemberId",
                schema: "Identity",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_User_TeamMembers_TeamMemberId",
                schema: "Identity",
                table: "User",
                column: "TeamMemberId",
                principalSchema: "Team",
                principalTable: "TeamMembers",
                principalColumn: "TeamMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_TeamMembers_TeamMemberId",
                schema: "Identity",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamMemberId",
                schema: "Identity",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_TeamMembers_TeamMemberId",
                schema: "Identity",
                table: "User",
                column: "TeamMemberId",
                principalSchema: "Team",
                principalTable: "TeamMembers",
                principalColumn: "TeamMemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
