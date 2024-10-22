using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuarios_UserId",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_UserId",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Publicaciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Publicaciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UserId",
                table: "Publicaciones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuarios_UserId",
                table: "Publicaciones",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
