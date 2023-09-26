using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameHistorymodeltobemoreaccurate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComboRank",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastCutFocus",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemorialCard",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrintRest",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMember",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMusic",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstSkill",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendHiScore",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SkillRank",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SynchroRank",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRank",
                table: "GameHistorySaintSnow",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComboRank",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastCutFocus",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemorialCard",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrintRest",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMember",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMusic",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstSkill",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendHiScore",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SkillRank",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SynchroRank",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRank",
                table: "GameHistoryAqours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComboRank",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastCutFocus",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemorialCard",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrintRest",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMember",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstMusic",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendFirstSkill",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecommendHiScore",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SkillRank",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SynchroRank",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRank",
                table: "GameHistory",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComboRank",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "LastCutFocus",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "MemorialCard",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "PrintRest",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMember",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMusic",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "RecommendFirstSkill",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "RecommendHiScore",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "SkillRank",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "SynchroRank",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "TotalRank",
                table: "GameHistorySaintSnow");

            migrationBuilder.DropColumn(
                name: "ComboRank",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "LastCutFocus",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "MemorialCard",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "PrintRest",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMember",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMusic",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "RecommendFirstSkill",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "RecommendHiScore",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "SkillRank",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "SynchroRank",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "TotalRank",
                table: "GameHistoryAqours");

            migrationBuilder.DropColumn(
                name: "ComboRank",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "LastCutFocus",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "MemorialCard",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "PrintRest",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMember",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "RecommendFirstMusic",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "RecommendFirstSkill",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "RecommendHiScore",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "SkillRank",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "SynchroRank",
                table: "GameHistory");

            migrationBuilder.DropColumn(
                name: "TotalRank",
                table: "GameHistory");
        }
    }
}
