using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class CamposRiesgoCubierto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Mercancia_Cotizacion_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.AddColumn<int>(
                name: "Administracion_Bien_ID",
                table: "Riesgo_Cubierto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Riesgo_Cubierto_Administracion_Bien_ID",
                table: "Riesgo_Cubierto",
                column: "Administracion_Bien_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgo_Cubierto_Tipo_Riesgo_ID",
                table: "Riesgo_Cubierto",
                column: "Tipo_Riesgo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Mercancia_Cotizacion_ID",
                table: "Cotizacion_Mercancia",
                column: "Cotizacion_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor",
                column: "Cotizacion_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgo_Cubierto_Administracion_Bien_Administracion_Bien_ID",
                table: "Riesgo_Cubierto",
                column: "Administracion_Bien_ID",
                principalTable: "Administracion_Bien",
                principalColumn: "Administracion_Bien_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgo_Cubierto_Tipo_Riesgo_Tipo_Riesgo_ID",
                table: "Riesgo_Cubierto",
                column: "Tipo_Riesgo_ID",
                principalTable: "Tipo_Riesgo",
                principalColumn: "Tipo_Riesgo_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgo_Cubierto_Administracion_Bien_Administracion_Bien_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.DropForeignKey(
                name: "FK_Riesgo_Cubierto_Tipo_Riesgo_Tipo_Riesgo_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.DropIndex(
                name: "IX_Riesgo_Cubierto_Administracion_Bien_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.DropIndex(
                name: "IX_Riesgo_Cubierto_Tipo_Riesgo_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Mercancia_Cotizacion_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Administracion_Bien_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Mercancia_Cotizacion_ID",
                table: "Cotizacion_Mercancia",
                column: "Cotizacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Cotizacion_ID",
                table: "Cotizacion_Contenedor",
                column: "Cotizacion_ID");
        }
    }
}
