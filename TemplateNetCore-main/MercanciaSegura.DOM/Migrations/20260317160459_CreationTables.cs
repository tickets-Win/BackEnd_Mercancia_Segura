using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class CreationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificado",
                columns: table => new
                {
                    Certificado_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: false),
                    Fecha_Certificado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificado", x => x.Certificado_ID);
                    table.ForeignKey(
                        name: "FK_Certificado_Cotizacion_Cotizacion_ID",
                        column: x => x.Cotizacion_ID,
                        principalTable: "Cotizacion",
                        principalColumn: "Cotizacion_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Evento",
                columns: table => new
                {
                    Tipo_Evento_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Evento", x => x.Tipo_Evento_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Siniestro",
                columns: table => new
                {
                    Tipo_Siniestro_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Siniestro", x => x.Tipo_Siniestro_ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoEndoso",
                columns: table => new
                {
                    Tipo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEndoso", x => x.Tipo_ID);
                });

            migrationBuilder.CreateTable(
                name: "Siniestros",
                columns: table => new
                {
                    Siniestro_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Certificado_ID = table.Column<int>(type: "int", nullable: false),
                    N_Reporte = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Fecha_Apertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Cierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tipo_Siniestro_ID = table.Column<int>(type: "int", nullable: true),
                    Mercancia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Lugar_De_Siniestro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Suma_Asegurada = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Monto_De_Reclamo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Monto_De_Indemnizacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tipo_de_evento_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siniestros", x => x.Siniestro_ID);
                    table.ForeignKey(
                        name: "FK_Siniestros_Certificado_Certificado_ID",
                        column: x => x.Certificado_ID,
                        principalTable: "Certificado",
                        principalColumn: "Certificado_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siniestros_Tipo_Siniestro_Tipo_Siniestro_ID",
                        column: x => x.Tipo_Siniestro_ID,
                        principalTable: "Tipo_Siniestro",
                        principalColumn: "Tipo_Siniestro_ID");
                });

            migrationBuilder.CreateTable(
                name: "Endosos",
                columns: table => new
                {
                    Endoso_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Endoso_ID = table.Column<int>(type: "int", nullable: false),
                    Numero_Endoso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Certificado_ID = table.Column<int>(type: "int", nullable: false),
                    Fecha_Elaboracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Agente = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Oficina = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    BeneficiarioPreferente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Moneda_ID = table.Column<int>(type: "int", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total_a_pagar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endosos", x => x.Endoso_ID);
                    table.ForeignKey(
                        name: "FK_Endosos_Certificado_Certificado_ID",
                        column: x => x.Certificado_ID,
                        principalTable: "Certificado",
                        principalColumn: "Certificado_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Endosos_TipoEndoso_Tipo_Endoso_ID",
                        column: x => x.Tipo_Endoso_ID,
                        principalTable: "TipoEndoso",
                        principalColumn: "Tipo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificado_Cotizacion_ID",
                table: "Certificado",
                column: "Cotizacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Endosos_Certificado_ID",
                table: "Endosos",
                column: "Certificado_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Endosos_Tipo_Endoso_ID",
                table: "Endosos",
                column: "Tipo_Endoso_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Siniestros_Certificado_ID",
                table: "Siniestros",
                column: "Certificado_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Siniestros_Tipo_Siniestro_ID",
                table: "Siniestros",
                column: "Tipo_Siniestro_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endosos");

            migrationBuilder.DropTable(
                name: "Siniestros");

            migrationBuilder.DropTable(
                name: "Tipo_Evento");

            migrationBuilder.DropTable(
                name: "TipoEndoso");

            migrationBuilder.DropTable(
                name: "Certificado");

            migrationBuilder.DropTable(
                name: "Tipo_Siniestro");
        }
    }
}
