using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addlivedatatodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiveDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Unlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    New = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalHiScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalHiScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalHiRate = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerCount1 = table.Column<int>(type: "INTEGER", nullable: false),
                    TrophyCountGold = table.Column<int>(type: "INTEGER", nullable: false),
                    TrophyCountSilver = table.Column<int>(type: "INTEGER", nullable: false),
                    TrophyCountBronze = table.Column<int>(type: "INTEGER", nullable: false),
                    FinaleCount = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveDatas_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveDatas_UserID",
                table: "LiveDatas",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiveDatas");
        }
    }
}
