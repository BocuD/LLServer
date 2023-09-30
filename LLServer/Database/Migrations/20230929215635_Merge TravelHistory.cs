using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class MergeTravelHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelHistoryAqours");

            migrationBuilder.DropTable(
                name: "TravelHistorySaintSnow");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TravelHistory",
                newName: "IdolKind");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdolKind",
                table: "TravelHistory",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "TravelHistoryAqours",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    OtherCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerBadge = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    OtherPlayerNameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    PrintRest = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapBackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelHistoryAqours", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_TravelHistoryAqours_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelHistorySaintSnow",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    OtherCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerBadge = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    OtherPlayerNameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    PrintRest = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapBackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelHistorySaintSnow", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_TravelHistorySaintSnow_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelHistoryAqours_UserID",
                table: "TravelHistoryAqours",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelHistorySaintSnow_UserID",
                table: "TravelHistorySaintSnow",
                column: "UserID");
        }
    }
}
