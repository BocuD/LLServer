using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LLServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    UserID = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdolKind = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NoteSpeedLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    SubMonitorType = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeBgm = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeSe = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeVoice = table.Column<int>(type: "INTEGER", nullable: false),
                    TenpoName = table.Column<string>(type: "TEXT", nullable: false),
                    PlayDate = table.Column<string>(type: "TEXT", nullable: false),
                    PlaySatellite = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCenter = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalExp = table.Column<int>(type: "INTEGER", nullable: false),
                    Honor = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameplate = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileCardId1 = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileCardId2 = table.Column<string>(type: "TEXT", nullable: false),
                    CreditCountSatellite = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditCountCenter = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayLs4 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.UserID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserData_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "UserData",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserData_UserId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.AlterColumn<ulong>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
