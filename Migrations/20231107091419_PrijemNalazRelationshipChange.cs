using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class PrijemNalazRelationshipChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem");

            migrationBuilder.DropIndex(
                name: "IX_Prijem_NalazID",
                table: "Prijem");

            migrationBuilder.DropColumn(
                name: "NalazID",
                table: "Prijem");

            migrationBuilder.AddColumn<int>(
                name: "PrijemID",
                table: "Nalaz",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz",
                column: "PrijemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Nalaz_Prijem_PrijemID",
                table: "Nalaz",
                column: "PrijemID",
                principalTable: "Prijem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nalaz_Prijem_PrijemID",
                table: "Nalaz");

            migrationBuilder.DropIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz");

            migrationBuilder.DropColumn(
                name: "PrijemID",
                table: "Nalaz");

            migrationBuilder.AddColumn<int>(
                name: "NalazID",
                table: "Prijem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prijem_NalazID",
                table: "Prijem",
                column: "NalazID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem",
                column: "NalazID",
                principalTable: "Nalaz",
                principalColumn: "ID");
        }
    }
}
