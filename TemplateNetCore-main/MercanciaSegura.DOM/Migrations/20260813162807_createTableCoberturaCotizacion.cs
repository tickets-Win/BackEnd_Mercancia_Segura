using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class createTableCoberturaCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cobertura_Cotizacion",
                columns: table => new
                {
                    Cobertura_Cotizacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cotizacion_ID = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobertura_Cotizacion", x => x.Cobertura_Cotizacion_ID);
                    table.ForeignKey(
                        name: "FK_Cobertura_Cotizacion_Cotizacion_Cotizacion_ID",
                        column: x => x.Cotizacion_ID,
                        principalTable: "Cotizacion",
                        principalColumn: "Cotizacion_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobertura_Cotizacion_Cotizacion_ID",
                table: "Cobertura_Cotizacion",
                column: "Cotizacion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cobertura_Cotizacion");
        }
    }
}
