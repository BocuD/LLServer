using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    /// <inheritdoc />
    public partial class Changerelationshipstructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GachaCards_GachaTables_GachaTableid",
                table: "GachaCards");

            migrationBuilder.DropIndex(
                name: "IX_GachaCards_GachaTableid",
                table: "GachaCards");

            migrationBuilder.DropColumn(
                name: "GachaTableid",
                table: "GachaCards");

            migrationBuilder.AddColumn<string>(
                name: "GachaCardid",
                table: "GachaTables",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "GachaCards",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

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

            migrationBuilder.CreateIndex(
                name: "IX_GachaTables_GachaCardid",
                table: "GachaTables",
                column: "GachaCardid");

            migrationBuilder.AddForeignKey(
                name: "FK_GachaTables_GachaCards_GachaCardid",
                table: "GachaTables",
                column: "GachaCardid",
                principalTable: "GachaCards",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GachaTables_GachaCards_GachaCardid",
                table: "GachaTables");

            migrationBuilder.DropTable(
                name: "GachaTableCard");

            migrationBuilder.DropIndex(
                name: "IX_GachaTables_GachaCardid",
                table: "GachaTables");

            migrationBuilder.DropColumn(
                name: "GachaCardid",
                table: "GachaTables");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "GachaCards",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "GachaTableid",
                table: "GachaCards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GachaCards_GachaTableid",
                table: "GachaCards",
                column: "GachaTableid");

            migrationBuilder.AddForeignKey(
                name: "FK_GachaCards_GachaTables_GachaTableid",
                table: "GachaCards",
                column: "GachaTableid",
                principalTable: "GachaTables",
                principalColumn: "id");
        }
    }
}
