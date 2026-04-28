using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clasificacion",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Total_Seguro_Mercancia",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Motivo_Cancelacion",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Suma_Asegurada",
                table: "Cotizacion");

            migrationBuilder.RenameColumn(
                name: "Sub_Clasificacion",
                table: "Cotizacion_Mercancia",
                newName: "SubClasificacion");

            migrationBuilder.RenameColumn(
                name: "Total_Tarifa",
                table: "Cotizacion_Contenedor",
                newName: "TC");

            migrationBuilder.RenameColumn(
                name: "Tarifa",
                table: "Cotizacion_Contenedor",
                newName: "Prima_Unitaria_USD");

            migrationBuilder.RenameColumn(
                name: "Tamaño_Tipo_Contendor",
                table: "Cotizacion_Contenedor",
                newName: "Referencia");

            migrationBuilder.RenameColumn(
                name: "Opcion_Cuota",
                table: "Cotizacion_Contenedor",
                newName: "Prima_Unitaria_MXN");

            migrationBuilder.RenameColumn(
                name: "IVA",
                table: "Cotizacion_Contenedor",
                newName: "LR");

            migrationBuilder.AddColumn<int>(
                name: "Clasificacion_ID",
                table: "Cotizacion_Mercancia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_Cotizar_ID",
                table: "Cotizacion_Mercancia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_Cuota_Aplicable_ID",
                table: "Cotizacion_Mercancia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_Cuota_Minima_ID",
                table: "Cotizacion_Mercancia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Suma_Asegurada",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Numero_contenedor",
                table: "Cotizacion_Contenedor",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cuota",
                table: "Cotizacion_Contenedor",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Beneficiario_Preferente_ID",
                table: "Cotizacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Cotizacion",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moneda_ID",
                table: "Cotizacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Cotizacion",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Cotizacion",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Cotizacion",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clasificacion",
                columns: table => new
                {
                    Clasificacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasificacion", x => x.Clasificacion_ID);
                });

            migrationBuilder.CreateTable(
                name: "Cuota_Contenedor",
                columns: table => new
                {
                    Cuota_Contenedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Cuota_ID = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Cotizacion_Contenedor_ID = table.Column<int>(type: "int", nullable: true),
                    Tipo_Tarifa_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota_Contenedor", x => x.Cuota_Contenedor_ID);
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Cotizacion_Contenedor_Cotizacion_Contenedor_ID",
                        column: x => x.Cotizacion_Contenedor_ID,
                        principalTable: "Cotizacion_Contenedor",
                        principalColumn: "Cotizacion_Contenedor_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Tipo_Cuota_Tipo_Cuota_ID",
                        column: x => x.Tipo_Cuota_ID,
                        principalTable: "Tipo_Cuota",
                        principalColumn: "Tipo_Cuota_ID");
                    table.ForeignKey(
                        name: "FK_Cuota_Contenedor_Tipo_Tarifa_Tipo_Tarifa_ID",
                        column: x => x.Tipo_Tarifa_ID,
                        principalTable: "Tipo_Tarifa",
                        principalColumn: "Tipo_Tarifa_ID");
                });

            migrationBuilder.CreateTable(
                name: "Tamanio_Contenedor",
                columns: table => new
                {
                    Tamanio_Contenedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanio_Contenedor", x => x.Tamanio_Contenedor_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Contenedor",
                columns: table => new
                {
                    Tipo_Contenedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Contenedor", x => x.Tipo_Contenedor_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Mercancia_Clasificacion_ID",
                table: "Cotizacion_Mercancia",
                column: "Clasificacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor",
                column: "Tamanio_Contendor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Contenedor_Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor",
                column: "Tipo_Contenedor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Beneficiario_Preferente_ID",
                table: "Cotizacion",
                column: "Beneficiario_Preferente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_Moneda_ID",
                table: "Cotizacion",
                column: "Moneda_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Cotizacion_Contenedor_ID",
                table: "Cuota_Contenedor",
                column: "Cotizacion_Contenedor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Tipo_Cuota_ID",
                table: "Cuota_Contenedor",
                column: "Tipo_Cuota_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Contenedor_Tipo_Tarifa_ID",
                table: "Cuota_Contenedor",
                column: "Tipo_Tarifa_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Beneficiario_Preferente_Beneficiario_Preferente_ID",
                table: "Cotizacion",
                column: "Beneficiario_Preferente_ID",
                principalTable: "Beneficiario_Preferente",
                principalColumn: "Beneficiario_Preferente_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Moneda_Moneda_ID",
                table: "Cotizacion",
                column: "Moneda_ID",
                principalTable: "Moneda",
                principalColumn: "Moneda_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Contenedor_Tamanio_Contenedor_Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor",
                column: "Tamanio_Contendor_ID",
                principalTable: "Tamanio_Contenedor",
                principalColumn: "Tamanio_Contenedor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Contenedor_Tipo_Contenedor_Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor",
                column: "Tipo_Contenedor_ID",
                principalTable: "Tipo_Contenedor",
                principalColumn: "Tipo_Contenedor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizacion_Mercancia_Clasificacion_Clasificacion_ID",
                table: "Cotizacion_Mercancia",
                column: "Clasificacion_ID",
                principalTable: "Clasificacion",
                principalColumn: "Clasificacion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Beneficiario_Preferente_Beneficiario_Preferente_ID",
                table: "Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Moneda_Moneda_ID",
                table: "Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Contenedor_Tamanio_Contenedor_Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Contenedor_Tipo_Contenedor_Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizacion_Mercancia_Clasificacion_Clasificacion_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropTable(
                name: "Clasificacion");

            migrationBuilder.DropTable(
                name: "Cuota_Contenedor");

            migrationBuilder.DropTable(
                name: "Tamanio_Contenedor");

            migrationBuilder.DropTable(
                name: "Tipo_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Mercancia_Clasificacion_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Contenedor_Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Beneficiario_Preferente_ID",
                table: "Cotizacion");

            migrationBuilder.DropIndex(
                name: "IX_Cotizacion_Moneda_ID",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Clasificacion_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Moneda_Cotizar_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Moneda_Cuota_Aplicable_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Moneda_Cuota_Minima_ID",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Suma_Asegurada",
                table: "Cotizacion_Mercancia");

            migrationBuilder.DropColumn(
                name: "Cuota",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Tamanio_Contendor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Tipo_Contenedor_ID",
                table: "Cotizacion_Contenedor");

            migrationBuilder.DropColumn(
                name: "Beneficiario_Preferente_ID",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Moneda_ID",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Cotizacion");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Cotizacion");

            migrationBuilder.RenameColumn(
                name: "SubClasificacion",
                table: "Cotizacion_Mercancia",
                newName: "Sub_Clasificacion");

            migrationBuilder.RenameColumn(
                name: "TC",
                table: "Cotizacion_Contenedor",
                newName: "Total_Tarifa");

            migrationBuilder.RenameColumn(
                name: "Referencia",
                table: "Cotizacion_Contenedor",
                newName: "Tamaño_Tipo_Contendor");

            migrationBuilder.RenameColumn(
                name: "Prima_Unitaria_USD",
                table: "Cotizacion_Contenedor",
                newName: "Tarifa");

            migrationBuilder.RenameColumn(
                name: "Prima_Unitaria_MXN",
                table: "Cotizacion_Contenedor",
                newName: "Opcion_Cuota");

            migrationBuilder.RenameColumn(
                name: "LR",
                table: "Cotizacion_Contenedor",
                newName: "IVA");

            migrationBuilder.AddColumn<string>(
                name: "Clasificacion",
                table: "Cotizacion_Mercancia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima_Servicio_De_Aseguramiento",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Seguro_Mercancia",
                table: "Cotizacion_Mercancia",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero_contenedor",
                table: "Cotizacion_Contenedor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Seguro_Contenedor",
                table: "Cotizacion_Contenedor",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motivo_Cancelacion",
                table: "Cotizacion",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Suma_Asegurada",
                table: "Cotizacion",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
