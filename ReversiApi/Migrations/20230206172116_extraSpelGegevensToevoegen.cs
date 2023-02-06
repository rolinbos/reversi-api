using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations
{
    public partial class extraSpelGegevensToevoegen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "spelerToken",
                table: "SpelGegevens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "SpelGegevens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "spelerToken",
                table: "SpelGegevens");

            migrationBuilder.DropColumn(
                name: "type",
                table: "SpelGegevens");
        }
    }
}
