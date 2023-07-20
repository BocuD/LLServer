using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addtraveldata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slot = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardMemorialId = table.Column<int>(type: "INTEGER", nullable: false),
                    Positions = table.Column<string>(type: "TEXT", nullable: false),
                    LastLandmark = table.Column<int>(type: "INTEGER", nullable: false),
                    Modified = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelData_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelHistory",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapBackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    OtherPlayerNameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerBadge = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    PrintRest = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelHistory", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_TravelHistory_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelHistoryAqours",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapBackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    OtherPlayerNameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerBadge = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    PrintRest = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    SnapBackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    OtherPlayerNameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherPlayerBadge = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateType = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    PrintRest = table.Column<bool>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TravelPamphlets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TravelPamphletId = table.Column<int>(type: "INTEGER", nullable: false),
                    Round = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalTalkCount = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDiceCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsNew = table.Column<bool>(type: "INTEGER", nullable: false),
                    TravelExRewards = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPamphlets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPamphlets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelData_UserID",
                table: "TravelData",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelHistory_UserID",
                table: "TravelHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelHistoryAqours_UserID",
                table: "TravelHistoryAqours",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelHistorySaintSnow_UserID",
                table: "TravelHistorySaintSnow",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPamphlets_UserID",
                table: "TravelPamphlets",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelData");

            migrationBuilder.DropTable(
                name: "TravelHistory");

            migrationBuilder.DropTable(
                name: "TravelHistoryAqours");

            migrationBuilder.DropTable(
                name: "TravelHistorySaintSnow");

            migrationBuilder.DropTable(
                name: "TravelPamphlets");
        }
    }
}
