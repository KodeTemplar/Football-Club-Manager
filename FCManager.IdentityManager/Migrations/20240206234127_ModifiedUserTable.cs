using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.IdentityManager.Migrations
{
    public partial class ModifiedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "PlayerNumber",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerNumber",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
