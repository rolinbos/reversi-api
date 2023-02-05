using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations
{
    public partial class addSpelerRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Speler1Read",
                table: "Spels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Speler2Read",
                table: "Spels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speler1Read",
                table: "Spels");

            migrationBuilder.DropColumn(
                name: "Speler2Read",
                table: "Spels");
        }
    }
}
