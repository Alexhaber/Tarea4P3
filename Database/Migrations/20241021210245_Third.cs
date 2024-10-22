using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Hora de creación",
                table: "Publicaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 17, 2, 45, 638, DateTimeKind.Local).AddTicks(957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Hora de creación",
                table: "Publicaciones",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 17, 2, 45, 638, DateTimeKind.Local).AddTicks(957));
        }
    }
}
