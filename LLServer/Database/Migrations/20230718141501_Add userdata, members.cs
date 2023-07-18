using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Adduserdatamembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserData_UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<ulong>(
                name: "UserID",
                table: "UserData",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "MemberData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    YellPoint = table.Column<int>(type: "INTEGER", nullable: false),
                    CardMemorialId = table.Column<int>(type: "INTEGER", nullable: false),
                    AchieveRank = table.Column<int>(type: "INTEGER", nullable: false),
                    Main = table.Column<int>(type: "INTEGER", nullable: false),
                    Camera = table.Column<int>(type: "INTEGER", nullable: false),
                    Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    New = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberData_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDataAqours",
                columns: table => new
                {
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileCardId1 = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileCardId2 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataAqours", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserDataAqours_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDataSaintSnow",
                columns: table => new
                {
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileCardId1 = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileCardId2 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataSaintSnow", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserDataSaintSnow_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberData_UserID",
                table: "MemberData",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Users_UserID",
                table: "UserData",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Users_UserID",
                table: "UserData");

            migrationBuilder.DropTable(
                name: "MemberData");

            migrationBuilder.DropTable(
                name: "UserDataAqours");

            migrationBuilder.DropTable(
                name: "UserDataSaintSnow");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserID",
                table: "UserData",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserData_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "UserData",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
