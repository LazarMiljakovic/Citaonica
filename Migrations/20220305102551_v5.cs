using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Profesor_ProfesorID",
                table: "Knjiga");

            migrationBuilder.DropIndex(
                name: "IX_Knjiga_ProfesorID",
                table: "Knjiga");

            migrationBuilder.DropColumn(
                name: "ProfesorID",
                table: "Knjiga");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Profesor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kancelarija",
                table: "Profesor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Profesor");

            migrationBuilder.DropColumn(
                name: "kancelarija",
                table: "Profesor");

            migrationBuilder.AddColumn<int>(
                name: "ProfesorID",
                table: "Knjiga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_ProfesorID",
                table: "Knjiga",
                column: "ProfesorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Profesor_ProfesorID",
                table: "Knjiga",
                column: "ProfesorID",
                principalTable: "Profesor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
