using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCManager.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.EnsureSchema(
                name: "Setting");

            migrationBuilder.EnsureSchema(
                name: "Team");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "Log",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "Setting",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "Team",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "TeamLogos",
                schema: "Team",
                columns: table => new
                {
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLogos", x => x.LogoId);
                    table.ForeignKey(
                        name: "FK_TeamLogos_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Team",
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                schema: "Team",
                columns: table => new
                {
                    TeamMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: true),
                    MemberType = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfSigning = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.TeamMemberId);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Team",
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMemberImages",
                schema: "Team",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMemberImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_TeamMemberImages_TeamMembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "Team",
                        principalTable: "TeamMembers",
                        principalColumn: "TeamMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamLogos_TeamId",
                schema: "Team",
                table: "TeamLogos",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberImages_TeamMemberId",
                schema: "Team",
                table: "TeamMemberImages",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                schema: "Team",
                table: "TeamMembers",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Log");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "TeamLogos",
                schema: "Team");

            migrationBuilder.DropTable(
                name: "TeamMemberImages",
                schema: "Team");

            migrationBuilder.DropTable(
                name: "TeamMembers",
                schema: "Team");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "Team");
        }
    }
}
