using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Fakultet_FakultetID",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Skripta_Fakultet_FakultetID",
                table: "Skripta");

            migrationBuilder.DropIndex(
                name: "IX_Skripta_FakultetID",
                table: "Skripta");

            migrationBuilder.DropIndex(
                name: "IX_Knjiga_FakultetID",
                table: "Knjiga");

            migrationBuilder.DropColumn(
                name: "FakultetID",
                table: "Skripta");

            migrationBuilder.DropColumn(
                name: "FakultetID",
                table: "Knjiga");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Knjiga",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FakultetID",
                table: "Skripta",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Knjiga",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FakultetID",
                table: "Knjiga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skripta_FakultetID",
                table: "Skripta",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_FakultetID",
                table: "Knjiga",
                column: "FakultetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Fakultet_FakultetID",
                table: "Knjiga",
                column: "FakultetID",
                principalTable: "Fakultet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skripta_Fakultet_FakultetID",
                table: "Skripta",
                column: "FakultetID",
                principalTable: "Fakultet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
