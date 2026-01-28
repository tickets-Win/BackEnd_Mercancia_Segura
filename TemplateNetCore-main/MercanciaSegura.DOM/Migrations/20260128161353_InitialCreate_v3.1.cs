using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Baja",
                table: "Vendedor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Prima_Total",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Prima_Neta",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Otros",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "IVA",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Derecho_Poliza",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Actualizacion",
                table: "Poliza",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Registro",
                table: "Poliza",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Baja",
                table: "Cliente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Aseguradora_ID",
                table: "Poliza",
                column: "Aseguradora_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Contratante_ID",
                table: "Poliza",
                column: "Contratante_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Forma_Pago_ID",
                table: "Poliza",
                column: "Forma_Pago_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Moneda_ID",
                table: "Poliza",
                column: "Moneda_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_Producto_ID",
                table: "Poliza",
                column: "Producto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_SubRamo_ID",
                table: "Poliza",
                column: "SubRamo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Aseguradora_Aseguradora_ID",
                table: "Poliza",
                column: "Aseguradora_ID",
                principalTable: "Aseguradora",
                principalColumn: "Aseguradora_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Contratante_Contratante_ID",
                table: "Poliza",
                column: "Contratante_ID",
                principalTable: "Contratante",
                principalColumn: "Contratante_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_FormaPago_Forma_Pago_ID",
                table: "Poliza",
                column: "Forma_Pago_ID",
                principalTable: "FormaPago",
                principalColumn: "FormaPago_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Moneda_Moneda_ID",
                table: "Poliza",
                column: "Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Producto_Producto_ID",
                table: "Poliza",
                column: "Producto_ID",
                principalTable: "Producto",
                principalColumn: "Producto_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_SubRamo_SubRamo_ID",
                table: "Poliza",
                column: "SubRamo_ID",
                principalTable: "SubRamo",
                principalColumn: "SubRamo_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Aseguradora_Aseguradora_ID",
                table: "Poliza");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Contratante_Contratante_ID",
                table: "Poliza");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_FormaPago_Forma_Pago_ID",
                table: "Poliza");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Moneda_Moneda_ID",
                table: "Poliza");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Producto_Producto_ID",
                table: "Poliza");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_SubRamo_SubRamo_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Aseguradora_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Contratante_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Forma_Pago_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Moneda_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_Producto_ID",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_SubRamo_ID",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Fecha_Baja",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Fecha_Actualizacion",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Fecha_Registro",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Fecha_Baja",
                table: "Cliente");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prima_Total",
                table: "Poliza",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Prima_Neta",
                table: "Poliza",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Otros",
                table: "Poliza",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "IVA",
                table: "Poliza",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Derecho_Poliza",
                table: "Poliza",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
