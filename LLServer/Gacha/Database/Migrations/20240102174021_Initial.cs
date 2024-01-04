using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GachaTables",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    isValid = table.Column<bool>(type: "INTEGER", nullable: false),
                    characterIds = table.Column<string>(type: "TEXT", nullable: false),
                    maxRarity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaTables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GachaCards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    characterIds = table.Column<string>(type: "TEXT", nullable: false),
                    rarity = table.Column<int>(type: "INTEGER", nullable: false),
                    GachaTableid = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaCards", x => x.id);
                    table.ForeignKey(
                        name: "FK_GachaCards_GachaTables_GachaTableid",
                        column: x => x.GachaTableid,
                        principalTable: "GachaTables",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GachaCards_GachaTableid",
                table: "GachaCards",
                column: "GachaTableid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GachaCards");

            migrationBuilder.DropTable(
                name: "GachaTables");
        }
    }
}
