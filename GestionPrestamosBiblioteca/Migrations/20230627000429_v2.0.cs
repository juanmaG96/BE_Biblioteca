using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPrestamosBiblioteca.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_UsuarioSimple_UsuarioId",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_UsuarioId",
                table: "Prestamo");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioSimpleId",
                table: "Prestamo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_UsuarioSimpleId",
                table: "Prestamo",
                column: "UsuarioSimpleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_UsuarioSimple_UsuarioSimpleId",
                table: "Prestamo",
                column: "UsuarioSimpleId",
                principalTable: "UsuarioSimple",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_UsuarioSimple_UsuarioSimpleId",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_UsuarioSimpleId",
                table: "Prestamo");

            migrationBuilder.DropColumn(
                name: "UsuarioSimpleId",
                table: "Prestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_UsuarioId",
                table: "Prestamo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_UsuarioSimple_UsuarioId",
                table: "Prestamo",
                column: "UsuarioId",
                principalTable: "UsuarioSimple",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
