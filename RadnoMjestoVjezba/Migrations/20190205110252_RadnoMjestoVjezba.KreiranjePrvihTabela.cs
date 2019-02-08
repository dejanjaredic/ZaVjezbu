using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RadnoMjestoVjezba.Migrations
{
    public partial class RadnoMjestoVjezbaKreiranjePrvihTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kancelarije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kancelarije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uredjaji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uredjaji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osobe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    KancelarijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osobe_Kancelarije_KancelarijaId",
                        column: x => x.KancelarijaId,
                        principalTable: "Kancelarije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisceniUredjaji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemeOd = table.Column<DateTime>(nullable: false),
                    VrijemeDo = table.Column<DateTime>(nullable: false),
                    UredjajId = table.Column<int>(nullable: false),
                    OsobaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisceniUredjaji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisceniUredjaji_Osobe_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisceniUredjaji_Uredjaji_UredjajId",
                        column: x => x.UredjajId,
                        principalTable: "Uredjaji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisceniUredjaji_OsobaId",
                table: "KorisceniUredjaji",
                column: "OsobaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisceniUredjaji_UredjajId",
                table: "KorisceniUredjaji",
                column: "UredjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Osobe_KancelarijaId",
                table: "Osobe",
                column: "KancelarijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisceniUredjaji");

            migrationBuilder.DropTable(
                name: "Osobe");

            migrationBuilder.DropTable(
                name: "Uredjaji");

            migrationBuilder.DropTable(
                name: "Kancelarije");
        }
    }
}
