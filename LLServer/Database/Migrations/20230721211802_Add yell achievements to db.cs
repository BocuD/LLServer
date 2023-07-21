using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addyellachievementstodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YellAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YellAchievementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Unlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    New = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YellAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YellAchievements_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YellAchievements_UserID",
                table: "YellAchievements",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YellAchievements");
        }
    }
}
