using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class TextosPolizaMercancia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Especiales",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exclusiones_Particulares",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medidas_De_Seguridad",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especiales",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Exclusiones_Particulares",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Medidas_De_Seguridad",
                table: "Poliza_Mercancia");
        }
    }
}
