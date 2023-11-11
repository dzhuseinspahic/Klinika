using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class PrijemMinorChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem");

            migrationBuilder.AlterColumn<int>(
                name: "NalazID",
                table: "Prijem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem",
                column: "NalazID",
                principalTable: "Nalaz",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem");

            migrationBuilder.AlterColumn<int>(
                name: "NalazID",
                table: "Prijem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prijem_Nalaz_NalazID",
                table: "Prijem",
                column: "NalazID",
                principalTable: "Nalaz",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
