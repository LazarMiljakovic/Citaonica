using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predmet_Profesor_ProfesorID",
                table: "Predmet");

            migrationBuilder.DropIndex(
                name: "IX_Predmet_ProfesorID",
                table: "Predmet");

            migrationBuilder.DropColumn(
                name: "ProfesorID",
                table: "Predmet");

            migrationBuilder.AddColumn<int>(
                name: "PredmetID",
                table: "Profesor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_PredmetID",
                table: "Profesor",
                column: "PredmetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesor_Predmet_PredmetID",
                table: "Profesor",
                column: "PredmetID",
                principalTable: "Predmet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesor_Predmet_PredmetID",
                table: "Profesor");

            migrationBuilder.DropIndex(
                name: "IX_Profesor_PredmetID",
                table: "Profesor");

            migrationBuilder.DropColumn(
                name: "PredmetID",
                table: "Profesor");

            migrationBuilder.AddColumn<int>(
                name: "ProfesorID",
                table: "Predmet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_ProfesorID",
                table: "Predmet",
                column: "ProfesorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Predmet_Profesor_ProfesorID",
                table: "Predmet",
                column: "ProfesorID",
                principalTable: "Profesor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
