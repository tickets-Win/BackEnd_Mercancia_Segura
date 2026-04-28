using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InserciónDeNIPoliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuota_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.AddColumn<string>(
                name: "Nombre_Interno_Poliza",
                table: "Cotizacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor",
                column: "Cotizacion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Nombre_Interno_Poliza",
                table: "Cotizacion");

            migrationBuilder.CreateTable(
                name: "Cuota_Contenedor",
                columns: table => new
                {
                    Cuota_Contenedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_Contenedor_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Cuota_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Tarifa_ID = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota_Contenedor", x => x.Cuota_Contenedor_ID);
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Cotizacion_Contenedor_Cotizacion_Contenedor_ID",
                        column: x => x.Cotizacion_Contenedor_ID,
                        principalTable: "Cotizacion_Contenedor",
                        principalColumn: "Cotizacion_Contenedor_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Tipo_Cuota_Tipo_Cuota_ID",
                        column: x => x.Tipo_Cuota_ID,
                        principalTable: "Tipo_Cuota",
                        principalColumn: "Tipo_Cuota_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Tipo_Tarifa_Tipo_Tarifa_ID",
                        column: x => x.Tipo_Tarifa_ID,
                        principalTable: "Tipo_Tarifa",
                        principalColumn: "Tipo_Tarifa_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor",
                column: "Cotizacion_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Cotizacion_Contenedor_ID",
                table: "Cuota_Contenedor",
                column: "Cotizacion_Contenedor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Tipo_Cuota_ID",
                table: "Cuota_Contenedor",
                column: "Tipo_Cuota_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Tipo_Tarifa_ID",
                table: "Cuota_Contenedor",
                column: "Tipo_Tarifa_ID");
        }
    }
}
