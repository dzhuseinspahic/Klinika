using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ljekar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Titula = table.Column<int>(type: "int", nullable: true),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ljekar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nalaz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DatumVrijemeKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nalaz", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacijent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Spol = table.Column<int>(type: "int", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prijem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVrijemePrijema = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacijentID = table.Column<int>(type: "int", nullable: false),
                    LjekarID = table.Column<int>(type: "int", nullable: false),
                    NalazID = table.Column<int>(type: "int", nullable: true),
                    HitniPrijem = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prijem_Ljekar_LjekarID",
                        column: x => x.LjekarID,
                        principalTable: "Ljekar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prijem_Nalaz_NalazID",
                        column: x => x.NalazID,
                        principalTable: "Nalaz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Prijem_Pacijent_PacijentID",
                        column: x => x.PacijentID,
                        principalTable: "Pacijent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prijem_LjekarID",
                table: "Prijem",
                column: "LjekarID");

            migrationBuilder.CreateIndex(
                name: "IX_Prijem_NalazID",
                table: "Prijem",
                column: "NalazID");

            migrationBuilder.CreateIndex(
                name: "IX_Prijem_PacijentID",
                table: "Prijem",
                column: "PacijentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prijem");

            migrationBuilder.DropTable(
                name: "Ljekar");

            migrationBuilder.DropTable(
                name: "Nalaz");

            migrationBuilder.DropTable(
                name: "Pacijent");
        }
    }
}
