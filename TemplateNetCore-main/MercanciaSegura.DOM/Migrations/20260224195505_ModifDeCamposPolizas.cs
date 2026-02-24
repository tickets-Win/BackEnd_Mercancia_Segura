using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class ModifDeCamposPolizas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Derecho_Poliza",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Otros",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Otros_Poliza",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Prima_Neta",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "Prima_Total",
                table: "Poliza");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Riesgo_Cubierto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ventas",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Maquila",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Indemnizacion_Otros",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Embarque_Filiales",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Deducibles",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Compras",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bienes_Usados",
                table: "Poliza_Mercancia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Derecho_Poliza",
                table: "Poliza_Mercancia",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Poliza_Mercancia",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Otro_Prima",
                table: "Poliza_Mercancia",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Neta",
                table: "Poliza_Mercancia",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Total",
                table: "Poliza_Mercancia",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Derecho_Poliza",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Maniobras_Rescate",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Otro_Prima",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Neta",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Total",
                table: "Poliza_Contenedor",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Derecho_Poliza",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Otro_Prima",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Prima_Neta",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Prima_Total",
                table: "Poliza_Mercancia");

            migrationBuilder.DropColumn(
                name: "Derecho_Poliza",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Maniobras_Rescate",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Otro_Prima",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Prima_Neta",
                table: "Poliza_Contenedor");

            migrationBuilder.DropColumn(
                name: "Prima_Total",
                table: "Poliza_Contenedor");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Riesgo_Cubierto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ventas",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Maquila",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Indemnizacion_Otros",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Embarque_Filiales",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Deducibles",
                table: "Poliza_Mercancia",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Compras",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bienes_Usados",
                table: "Poliza_Mercancia",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Derecho_Poliza",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Otros",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Otros_Poliza",
                table: "Poliza",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Neta",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Total",
                table: "Poliza",
                type: "decimal(10,2)",
                nullable: true);
        }
    }
}
