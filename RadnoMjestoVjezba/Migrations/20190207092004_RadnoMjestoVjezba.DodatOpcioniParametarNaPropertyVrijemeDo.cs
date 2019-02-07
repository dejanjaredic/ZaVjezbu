using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RadnoMjestoVjezba.Migrations
{
    public partial class RadnoMjestoVjezbaDodatOpcioniParametarNaPropertyVrijemeDo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VrijemeDo",
                table: "KorisceniUredjaji",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VrijemeDo",
                table: "KorisceniUredjaji",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
