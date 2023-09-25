using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Storegamehistoryindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameHistory",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayPlace = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    DUserId = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedMemberCard = table.Column<int>(type: "INTEGER", nullable: false),
                    YellRank = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillCardsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillArgCamera = table.Column<string>(type: "TEXT", nullable: false),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventMode = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPart = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteMissCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteBadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGreatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NotePerfectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistory", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_GameHistory_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameHistoryAqours",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayPlace = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    DUserId = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedMemberCard = table.Column<int>(type: "INTEGER", nullable: false),
                    YellRank = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillCardsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillArgCamera = table.Column<string>(type: "TEXT", nullable: false),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventMode = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPart = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteMissCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteBadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGreatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NotePerfectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistoryAqours", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_GameHistoryAqours_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameHistorySaintSnow",
                columns: table => new
                {
                    DbId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayPlace = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    DUserId = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedMemberCard = table.Column<int>(type: "INTEGER", nullable: false),
                    YellRank = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillCardsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillArgCamera = table.Column<string>(type: "TEXT", nullable: false),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventMode = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPart = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteMissCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteBadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGreatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NotePerfectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistorySaintSnow", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_GameHistorySaintSnow_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameHistory_UserID",
                table: "GameHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistoryAqours_UserID",
                table: "GameHistoryAqours",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistorySaintSnow_UserID",
                table: "GameHistorySaintSnow",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameHistory");

            migrationBuilder.DropTable(
                name: "GameHistoryAqours");

            migrationBuilder.DropTable(
                name: "GameHistorySaintSnow");
        }
    }
}
