using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v71 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Riesgos",
                columns: table => new
                {
                    Riesgo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_ID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgos", x => x.Riesgo_ID);
                    table.ForeignKey(
                        name: "FK_Riesgos_Producto_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Producto",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Cobertura_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Riesgo_ID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Cobertura_ID);
                    table.ForeignKey(
                        name: "FK_Coberturas_Riesgos_Riesgo_ID",
                        column: x => x.Riesgo_ID,
                        principalTable: "Riesgos",
                        principalColumn: "Riesgo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliza_Cobertura",
                columns: table => new
                {
                    Poliza_Cobertura_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cobertura_ID = table.Column<int>(type: "int", nullable: false),
                    Poliza_ID = table.Column<int>(type: "int", nullable: false),
                    Dedicuble = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Prima = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    Suma_Asegurada = table.Column<decimal>(type: "decimal(14,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza_Cobertura", x => x.Poliza_Cobertura_ID);
                    table.ForeignKey(
                        name: "FK_Poliza_Cobertura_Coberturas_Cobertura_ID",
                        column: x => x.Cobertura_ID,
                        principalTable: "Coberturas",
                        principalColumn: "Cobertura_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poliza_Cobertura_Poliza_Poliza_ID",
                        column: x => x.Poliza_ID,
                        principalTable: "Poliza",
                        principalColumn: "Poliza_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coberturas_Riesgo_ID",
                table: "Coberturas",
                column: "Riesgo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Cobertura_ID",
                table: "Poliza_Cobertura",
                column: "Cobertura_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Poliza_ID",
                table: "Poliza_Cobertura",
                column: "Poliza_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgos_Producto_ID",
                table: "Riesgos",
                column: "Producto_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poliza_Cobertura");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Riesgos");
        }
    }
}
