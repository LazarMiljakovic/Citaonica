using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Godina",
                table: "Knjiga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Godina",
                table: "Knjiga",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
