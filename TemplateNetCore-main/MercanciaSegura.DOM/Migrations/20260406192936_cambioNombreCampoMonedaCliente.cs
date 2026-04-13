using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class cambioNombreCampoMonedaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Internacional_ID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Nacional_ID",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "Moneda_Cuota_Nacional_ID",
                table: "Cliente",
                newName: "Cuota_Minima_Moneda_ID");

            migrationBuilder.RenameColumn(
                name: "Moneda_Cuota_Internacional_ID",
                table: "Cliente",
                newName: "Cuota_Aplicable_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Moneda_Cuota_Nacional_ID",
                table: "Cliente",
                newName: "IX_Cliente_Cuota_Minima_Moneda_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Moneda_Cuota_Internacional_ID",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Moneda_Cuota_Nacional_ID");

            migrationBuilder.RenameColumn(
                name: "Cuota_Aplicable_Moneda_ID",
                table: "Cliente",
                newName: "Moneda_Cuota_Internacional_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Minima_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Moneda_Cuota_Nacional_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Cuota_Aplicable_Moneda_ID",
                table: "Cliente",
                newName: "IX_Cliente_Moneda_Cuota_Internacional_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Internacional_ID",
                table: "Cliente",
                column: "Moneda_Cuota_Internacional_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Nacional_ID",
                table: "Cliente",
                column: "Moneda_Cuota_Nacional_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");
        }
    }
}
