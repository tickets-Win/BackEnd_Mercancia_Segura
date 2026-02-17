using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v811 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgo_Producto_Producto_ID",
                table: "Riesgo");

            migrationBuilder.DropIndex(
                name: "IX_Riesgo_Producto_ID",
                table: "Riesgo");

            migrationBuilder.DropColumn(
                name: "Producto_ID",
                table: "Riesgo");

            migrationBuilder.AddColumn<string>(
                name: "Clave_Agente",
                table: "Poliza",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Folio_Poliza",
                table: "Poliza",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clave_Agente",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Folio_Poliza",
                table: "Poliza");

            migrationBuilder.AddColumn<int>(
                name: "Producto_ID",
                table: "Riesgo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Riesgo_Producto_ID",
                table: "Riesgo",
                column: "Producto_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgo_Producto_Producto_ID",
                table: "Riesgo",
                column: "Producto_ID",
                principalTable: "Producto",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
