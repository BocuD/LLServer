using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Cardgroupstotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cardGroupIds",
                table: "GachaTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cardGroupIds",
                table: "GachaTables");
        }
    }
}
