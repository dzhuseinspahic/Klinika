using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class PacijentKnjizicaCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BrojZdravstveneKnjizice",
                table: "Pacijent",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BrojZdravstveneKnjizice",
                table: "Pacijent",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
