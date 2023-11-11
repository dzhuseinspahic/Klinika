using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    /// <inheritdoc />
    public partial class OneToOnePN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz");

            migrationBuilder.CreateIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz",
                column: "PrijemID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz");

            migrationBuilder.CreateIndex(
                name: "IX_Nalaz_PrijemID",
                table: "Nalaz",
                column: "PrijemID");
        }
    }
}
