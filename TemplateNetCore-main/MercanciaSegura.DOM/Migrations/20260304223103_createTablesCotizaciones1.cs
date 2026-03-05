using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class createTablesCotizaciones1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "Beneficiario_Preferente");

            migrationBuilder.CreateTable(
                name: "Cotizacion",
                columns: table => new
                {
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poliza_ID = table.Column<int>(type: "int", nullable: true),
                    Fecha_Cotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente_ID = table.Column<int>(type: "int", nullable: true),
                    Vigencia_Del = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vigencia_Hasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Suma_Asegurada = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Gastos_Expedicion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Motivo_Cancelacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Fecha_Cancelacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizacion", x => x.Cotizacion_ID);
                    table.ForeignKey(
                        name: "FK_Cotizacion_Cliente_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID");
                    table.ForeignKey(
                        name: "FK_Cotizacion_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID");
                });

            migrationBuilder.CreateTable(
                name: "Cotizacion_Contenedor",
                columns: table => new
                {
                    Cotizacion_Contenedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: false),
                    Tamaño_Tipo_Contendor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Numero_contenedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Opcion_Cuota = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tarifa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total_Tarifa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizacion_Contenedor", x => x.Cotizacion_Contenedor_ID);
                    table.ForeignKey(
                        name: "FK_Cotizacion_Contenedor_Cotizacion_Cotizacion_ID",
                        column: x => x.Cotizacion_ID,
                        principalTable: "Cotizacion",
                        principalColumn: "Cotizacion_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotizacion_Mercancia",
                columns: table => new
                {
                    Cotizacion_Mercancia_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: false),
                    Cotizacion_Cliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Transito = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Clasificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sub_Clasificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion_Mercancia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_Empaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medio_De_Conduccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medio_De_Transporte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medidas_De_Seguridad_Adicionales = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deducibles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cuota_Aplicable = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Cuota_Minima = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Tipo_Cambio_Cotizar = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Prima_Servicio_De_Aseguramiento = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Total_Seguro_Mercancia = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Total_Seguro_Contenedor = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizacion_Mercancia", x => x.Cotizacion_Mercancia_ID);
                    table.ForeignKey(
                        name: "FK_Cotizacion_Mercancia_Cotizacion_Cotizacion_ID",
                        column: x => x.Cotizacion_ID,
                        principalTable: "Cotizacion",
                        principalColumn: "Cotizacion_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Cliente_ID",
                table: "Cotizacion",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Poliza_ID",
                table: "Cotizacion",
                column: "Poliza_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor",
                column: "Cotizacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Mercancia_Cotizacion_ID",
                table: "Cotizacion_Mercancia",
                column: "Cotizacion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotizacion_Contenedor");

            migrationBuilder.DropTable(
                name: "Cotizacion_Mercancia");

            migrationBuilder.DropTable(
                name: "Cotizacion");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "Beneficiario_Preferente",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
