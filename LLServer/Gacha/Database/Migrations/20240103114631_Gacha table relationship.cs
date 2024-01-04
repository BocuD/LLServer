using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Gachatablerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GachaTableCard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GachaTableCard",
                columns: table => new
                {
                    GachaTableId = table.Column<string>(type: "TEXT", nullable: false),
                    GachaCardId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaTableCard", x => new { x.GachaTableId, x.GachaCardId });
                    table.ForeignKey(
                        name: "FK_GachaTableCard_GachaTables_GachaTableId",
                        column: x => x.GachaTableId,
                        principalTable: "GachaTables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
