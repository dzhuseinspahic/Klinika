using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class PacijentKnjizicaFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrojZdravstveneKnjizice",
                table: "Pacijent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojZdravstveneKnjizice",
                table: "Pacijent");
        }
    }
}
