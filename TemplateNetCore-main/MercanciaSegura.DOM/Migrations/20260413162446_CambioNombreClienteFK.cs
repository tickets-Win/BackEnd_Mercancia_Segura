using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombreClienteFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Aplicable_Moneda_ID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Moneda_ID",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "Cuota_Minima_Moneda_ID",
                table: "Cliente",
                newName: "Cuota_Minima_Nacional_Moneda_ID");

            migrationBuilder.RenameColumn(
                name: "Cuota_Aplicable_Moneda_ID",
                table: "Cliente",
                newName: "Cuota_Minima_Internacional_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Minima_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Cuota_Minima_Nacional_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Aplicable_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Cuota_Minima_Internacional_Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Internacional_Moneda_ID",
                table: "Cliente",
                column: "Cuota_Minima_Internacional_Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Nacional_Moneda_ID",
                table: "Cliente",
                column: "Cuota_Minima_Nacional_Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Internacional_Moneda_ID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Nacional_Moneda_ID",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "Cuota_Minima_Nacional_Moneda_ID",
                table: "Cliente",
                newName: "Cuota_Minima_Moneda_ID");

            migrationBuilder.RenameColumn(
                name: "Cuota_Minima_Internacional_Moneda_ID",
                table: "Cliente",
                newName: "Cuota_Aplicable_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Minima_Nacional_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Cuota_Minima_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Minima_Internacional_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Cuota_Aplicable_Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Aplicable_Moneda_ID",
                table: "Cliente",
                column: "Cuota_Aplicable_Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Cuota_Minima_Moneda_ID",
                table: "Cliente",
                column: "Cuota_Minima_Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");
        }
    }
}
