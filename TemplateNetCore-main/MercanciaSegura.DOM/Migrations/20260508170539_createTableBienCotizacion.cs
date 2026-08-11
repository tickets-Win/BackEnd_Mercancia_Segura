using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class createTableBienCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bien_Cotizacion",
                columns: table => new
                {
                    Bien_Cotizacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: true),
                    Administracion_Bien_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Bien = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bien_Cotizacion", x => x.Bien_Cotizacion_ID);
                    table.ForeignKey(
                        name: "FK_Bien_Cotizacion_Administracion_Bien_Administracion_Bien_ID",
                        column: x => x.Administracion_Bien_ID,
                        principalTable: "Administracion_Bien",
                        principalColumn: "Administracion_Bien_ID");
                    table.ForeignKey(
                        name: "FK_Bien_Cotizacion_Cotizacion_Cotizacion_ID",
                        column: x => x.Cotizacion_ID,
                        principalTable: "Cotizacion",
                        principalColumn: "Cotizacion_ID");
                    table.ForeignKey(
                        name: "FK_Bien_Cotizacion_Tipo_Bien_Tipo_Bien",
                        column: x => x.Tipo_Bien,
                        principalTable: "Tipo_Bien",
                        principalColumn: "Tipo_Bien_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Cotizacion_Administracion_Bien_ID",
                table: "Bien_Cotizacion",
                column: "Administracion_Bien_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Cotizacion_Cotizacion_ID",
                table: "Bien_Cotizacion",
                column: "Cotizacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Cotizacion_Tipo_Bien",
                table: "Bien_Cotizacion",
                column: "Tipo_Bien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bien_Cotizacion");
        }
    }
}
