using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.IdentityManager.Migrations
{
    public partial class ModifiedUserTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerNumber",
                schema: "Identity",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerNumber",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: true);
        }
    }
}
