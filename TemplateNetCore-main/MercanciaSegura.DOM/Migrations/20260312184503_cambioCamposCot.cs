using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class cambioCamposCot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Mercancia");

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Contenedor",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Contenedor");

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
