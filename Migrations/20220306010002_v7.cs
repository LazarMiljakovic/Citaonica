using Microsoft.EntityFrameworkCore.Migrations;

namespace Citaonica.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Fakultet");

            migrationBuilder.AddColumn<int>(
                name: "GradID",
                table: "Fakultet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fakultet_GradID",
                table: "Fakultet",
                column: "GradID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultet_Grad_GradID",
                table: "Fakultet",
                column: "GradID",
                principalTable: "Grad",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fakultet_Grad_GradID",
                table: "Fakultet");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropIndex(
                name: "IX_Fakultet_GradID",
                table: "Fakultet");

            migrationBuilder.DropColumn(
                name: "GradID",
                table: "Fakultet");

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Fakultet",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
