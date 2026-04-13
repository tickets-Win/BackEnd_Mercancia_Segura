using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposMonedaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Tipo_Cambio_Ventanilla",
                table: "Moneda",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Tipo_Cambio",
                table: "Moneda",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_Cuota_Internacional_ID",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_Cuota_Nacional_ID",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Moneda_Cuota_Internacional_ID",
                table: "Cliente",
                column: "Moneda_Cuota_Internacional_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Moneda_Cuota_Nacional_ID",
                table: "Cliente",
                column: "Moneda_Cuota_Nacional_ID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Internacional_ID",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Moneda_Moneda_Cuota_Nacional_ID",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Moneda_Cuota_Internacional_ID",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Moneda_Cuota_Nacional_ID",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Moneda_Cuota_Internacional_ID",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Moneda_Cuota_Nacional_ID",
                table: "Cliente");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tipo_Cambio_Ventanilla",
                table: "Moneda",
                type: "decimal(10,2)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Tipo_Cambio",
                table: "Moneda",
                type: "decimal(10,2)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);
        }
    }
}
