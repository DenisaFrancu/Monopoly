using Microsoft.EntityFrameworkCore.Migrations;

namespace Monopoly.Migrations.GameRoom
{
    public partial class UpdateConnectionIdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "ConnectionIds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "ConnectionIds");
        }
    }
}
