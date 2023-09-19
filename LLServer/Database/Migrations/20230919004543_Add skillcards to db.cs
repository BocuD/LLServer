using LLServer.Models.UserData;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addskillcardstodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillCardData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardSkillId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    PrintRest = table.Column<int>(type: "INTEGER", nullable: false),
                    New = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCardData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillCardData_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillCardData_UserID",
                table: "SkillCardData",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillCardData");
        }
    }
}
