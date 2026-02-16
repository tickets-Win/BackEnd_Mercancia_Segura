using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Cobertura_Coberturas_Cobertura_ID",
                table: "Poliza_Cobertura");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Riesgos");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Cobertura_Cobertura_ID",
                table: "Poliza_Cobertura");

            migrationBuilder.DropColumn(
                name: "Cobertura_ID",
                table: "Poliza_Cobertura");

            migrationBuilder.CreateTable(
                name: "Riesgo",
                columns: table => new
                {
                    Riesgo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_ID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgo", x => x.Riesgo_ID);
                    table.ForeignKey(
                        name: "FK_Riesgo_Producto_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Producto",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cobertura",
                columns: table => new
                {
                    Cobertura_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Riesgo_ID = table.Column<int>(type: "int", nullable: false),
                    Poliza_Cobertura_ID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobertura", x => x.Cobertura_ID);
                    table.ForeignKey(
                        name: "FK_Cobertura_Poliza_Cobertura_Poliza_Cobertura_ID",
                        column: x => x.Poliza_Cobertura_ID,
                        principalTable: "Poliza_Cobertura",
                        principalColumn: "Poliza_Cobertura_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cobertura_Riesgo_Riesgo_ID",
                        column: x => x.Riesgo_ID,
                        principalTable: "Riesgo",
                        principalColumn: "Riesgo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobertura_Poliza_Cobertura_ID",
                table: "Cobertura",
                column: "Poliza_Cobertura_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cobertura_Riesgo_ID",
                table: "Cobertura",
                column: "Riesgo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgo_Producto_ID",
                table: "Riesgo",
                column: "Producto_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cobertura");

            migrationBuilder.DropTable(
                name: "Riesgo");

            migrationBuilder.AddColumn<int>(
                name: "Cobertura_ID",
                table: "Poliza_Cobertura",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Cobertura_Cobertura_ID",
                table: "Poliza_Cobertura",
                column: "Cobertura_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Coberturas_Riesgo_ID",
                table: "Coberturas",
                column: "Riesgo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgos_Producto_ID",
                table: "Riesgos",
                column: "Producto_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Cobertura_Coberturas_Cobertura_ID",
                table: "Poliza_Cobertura",
                column: "Cobertura_ID",
                principalTable: "Coberturas",
                principalColumn: "Cobertura_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
