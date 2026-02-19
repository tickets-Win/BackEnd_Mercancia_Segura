using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class v12_Poliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.AddColumn<int>(
                name: "Administracion_Bien_ID",
                table: "Poliza_Mercancia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia",
                column: "Poliza_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Administracion_Bien_ID",
                table: "Poliza_Mercancia");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Mercancia_Poliza_ID",
                table: "Poliza_Mercancia",
                column: "Poliza_ID",
                unique: true);
        }
    }
}
