using Microsoft.EntityFrameworkCore.Migrations;

namespace Monopoly.Migrations.GameRoom
{
    public partial class InitialBaseline2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersIds");

            migrationBuilder.CreateTable(
                name: "ConnectionIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    connectionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionIds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionIds");

            migrationBuilder.CreateTable(
                name: "PlayersIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    connectionPlayer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    connectionPlayer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    connectionPlayer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    connectionPlayer4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersIds", x => x.Id);
                });
        }
    }
}
