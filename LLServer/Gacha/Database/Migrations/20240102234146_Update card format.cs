using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Updatecardformat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rarity",
                table: "GachaCards");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "GachaCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "rarityIds",
                table: "GachaCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "GachaCards");

            migrationBuilder.DropColumn(
                name: "rarityIds",
                table: "GachaCards");

            migrationBuilder.AddColumn<int>(
                name: "rarity",
                table: "GachaCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
