using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class createTableTransitoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transito",
                table: "Cotizacion_Mercancia");

            migrationBuilder.AddColumn<int>(
                name: "Transito_ID",
                table: "Cotizacion_Mercancia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Mercancia_Transito_ID",
                table: "Cotizacion_Mercancia",
                column: "Transito_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Mercancia_Transito_Transito_ID",
                table: "Cotizacion_Mercancia",
                column: "Transito_ID",
                principalTable: "Transito",
                principalColumn: "Transito_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Mercancia_Transito_Transito_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Mercancia_Transito_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Transito_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.AddColumn<string>(
                name: "Transito",
                table: "Cotizacion_Mercancia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
