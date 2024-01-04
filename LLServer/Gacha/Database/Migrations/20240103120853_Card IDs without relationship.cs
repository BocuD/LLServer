using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class CardIDswithoutrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GachaCardGachaTable");

            migrationBuilder.AddColumn<string>(
                name: "cardIds",
                table: "GachaTables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cardIds",
                table: "GachaTables");

            migrationBuilder.CreateTable(
                name: "GachaCardGachaTable",
                columns: table => new
                {
                    cardsid = table.Column<string>(type: "TEXT", nullable: false),
                    gachaTablesid = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaCardGachaTable", x => new { x.cardsid, x.gachaTablesid });
                    table.ForeignKey(
                        name: "FK_GachaCardGachaTable_GachaCards_cardsid",
                        column: x => x.cardsid,
                        principalTable: "GachaCards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GachaCardGachaTable_GachaTables_gachaTablesid",
                        column: x => x.gachaTablesid,
                        principalTable: "GachaTables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GachaCardGachaTable_gachaTablesid",
                table: "GachaCardGachaTable",
                column: "gachaTablesid");
        }
    }
}
