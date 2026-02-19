using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class v11_Poliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Riesgo_Cubierto_Poliza_Mercancia_ID",
                table: "Riesgo_Cubierto",
                column: "Poliza_Mercancia_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgo_Cubierto_Poliza_Mercancia_Poliza_Mercancia_ID",
                table: "Riesgo_Cubierto",
                column: "Poliza_Mercancia_ID",
                principalTable: "Poliza_Mercancia",
                principalColumn: "Poliza_Mercancia_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgo_Cubierto_Poliza_Mercancia_Poliza_Mercancia_ID",
                table: "Riesgo_Cubierto");

            migrationBuilder.DropIndex(
                name: "IX_Riesgo_Cubierto_Poliza_Mercancia_ID",
                table: "Riesgo_Cubierto");
        }
    }
}
