using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfesorID",
                table: "Predmet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesorID",
                table: "Knjiga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakultetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Profesor_Fakultet_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predmet_ProfesorID",
                table: "Predmet",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_ProfesorID",
                table: "Knjiga",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_FakultetID",
                table: "Profesor",
                column: "FakultetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Profesor_ProfesorID",
                table: "Knjiga",
                column: "ProfesorID",
                principalTable: "Profesor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Predmet_Profesor_ProfesorID",
                table: "Predmet",
                column: "ProfesorID",
                principalTable: "Profesor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Profesor_ProfesorID",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Predmet_Profesor_ProfesorID",
                table: "Predmet");

            migrationBuilder.DropTable(
                name: "Profesor");

            migrationBuilder.DropIndex(
                name: "IX_Predmet_ProfesorID",
                table: "Predmet");

            migrationBuilder.DropIndex(
                name: "IX_Knjiga_ProfesorID",
                table: "Knjiga");

            migrationBuilder.DropColumn(
                name: "ProfesorID",
                table: "Predmet");

            migrationBuilder.DropColumn(
                name: "ProfesorID",
                table: "Knjiga");
        }
    }
}
