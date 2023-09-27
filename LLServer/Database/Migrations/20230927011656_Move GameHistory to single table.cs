using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class MoveGameHistorytosingletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameHistoryAqours");

            migrationBuilder.DropTable(
                name: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameHistory");

            migrationBuilder.AlterColumn<int>(
                name: "GameHistoryId",
                table: "ProfileCards",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DbId",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameHistoryId",
                table: "ProfileCards",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "DbId",
                table: "GameHistory",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "GameHistory",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GameHistoryAqours",
                columns: table => new
                {
                    DbId = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboRank = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    DUserId = table.Column<string>(type: "TEXT", nullable: false),
                    EventMode = table.Column<int>(type: "INTEGER", nullable: false),
                    Favorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LastCutFocus = table.Column<int>(type: "INTEGER", nullable: false),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MemorialCard = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteBadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGreatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteMissCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NotePerfectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPart = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPlace = table.Column<string>(type: "TEXT", nullable: false),
                    PrintRest = table.Column<int>(type: "INTEGER", nullable: false),
                    RecommendFirstMember = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendFirstMusic = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendFirstSkill = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendHiScore = table.Column<bool>(type: "INTEGER", nullable: false),
                    SkillArgCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillRank = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillStatusCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusStage = table.Column<string>(type: "TEXT", nullable: false),
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroRank = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalRank = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedMemberCard = table.Column<int>(type: "INTEGER", nullable: false),
                    YellRank = table.Column<int>(type: "INTEGER", nullable: false)
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
                    DbId = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboRank = table.Column<int>(type: "INTEGER", nullable: false),
                    ComboScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    DUserId = table.Column<string>(type: "TEXT", nullable: false),
                    EventMode = table.Column<int>(type: "INTEGER", nullable: false),
                    Favorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalePoint = table.Column<int>(type: "INTEGER", nullable: false),
                    FullCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LastCutFocus = table.Column<int>(type: "INTEGER", nullable: false),
                    LiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCombo = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MemorialCard = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteBadCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteGreatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteMissCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NotePerfectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPart = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayPlace = table.Column<string>(type: "TEXT", nullable: false),
                    PrintRest = table.Column<int>(type: "INTEGER", nullable: false),
                    RecommendFirstMember = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendFirstMusic = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendFirstSkill = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecommendHiScore = table.Column<bool>(type: "INTEGER", nullable: false),
                    SkillArgCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillCardsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillLevelsStage = table.Column<string>(type: "TEXT", nullable: false),
                    SkillRank = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillScore = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillStatusCamera = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusMain = table.Column<string>(type: "TEXT", nullable: false),
                    SkillStatusStage = table.Column<string>(type: "TEXT", nullable: false),
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroRank = table.Column<int>(type: "INTEGER", nullable: false),
                    SynchroScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalRank = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnicalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalRank = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedMemberCard = table.Column<int>(type: "INTEGER", nullable: false),
                    YellRank = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_GameHistoryAqours_UserID",
                table: "GameHistoryAqours",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistorySaintSnow_UserID",
                table: "GameHistorySaintSnow",
                column: "UserID");
        }
    }
}
