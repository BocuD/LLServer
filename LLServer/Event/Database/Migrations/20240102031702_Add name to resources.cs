using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Event.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addnametoresources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Resources",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Resources");
        }
    }
}
