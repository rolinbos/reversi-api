using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations
{
    public partial class addSpelGegevens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpelGegevens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    spelToken = table.Column<string>(type: "TEXT", nullable: false),
                    datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    waarde = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpelGegevens", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpelGegevens");
        }
    }
}
