using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class SnapshotCotizacionYNoAplica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "No_Aplica_Cuotas_Especiales",
                table: "Poliza_Mercancia",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condiciones_Especiales",
                table: "Cotizacion_Mercancia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exclusiones",
                table: "Cotizacion_Mercancia",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No_Aplica_Cuotas_Especiales",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Condiciones_Especiales",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Exclusiones",
                table: "Cotizacion_Mercancia");
        }
    }
}
