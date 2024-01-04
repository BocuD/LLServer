using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addfieldforcardtypetogacha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cardType",
                table: "GachaCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cardType",
                table: "GachaCards");
        }
    }
}
