using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <inheritdoc />
    public partial class SeedCatalogosContenedorYNumeroTexto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Los numeros de contenedor son alfanumericos (ISO 6346: 4 letras y
            // 7 digitos, por ejemplo MSCU1234567), no caben en un int.
            migrationBuilder.AlterColumn<string>(
                name: "Numero_contenedor",
                table: "Cotizacion_Contenedor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Catalogos que estaban vacios. Estos son los valores que la pantalla
            // de cotizaciones tenia fijos en JavaScript.
            migrationBuilder.InsertData(
                table: "Tipo_Contenedor",
                columns: new[] { "Tipo_Contenedor_ID", "Nombre" },
                values: new object[,]
                {
                    { 1, "DC" },
                    { 2, "VENTILATED" },
                    { 3, "OT" },
                    { 4, "OT HC" },
                    { 5, "HC" },
                    { 6, "FR" },
                    { 7, "FR HC" },
                    { 8, "PLATAFORMA" },
                    { 9, "HARD TOP" },
                    { 10, "REEFER" },
                    { 11, "REEFER HC" },
                    { 12, "REEFER HC (Control Atmosf.)" },
                    { 13, "ISOTANQUE" }
                });

            migrationBuilder.InsertData(
                table: "Tamanio_Contenedor",
                columns: new[] { "Tamanio_Contenedor_ID", "Nombre" },
                values: new object[,]
                {
                    { 1, "20'" },
                    { 2, "40'" },
                    { 3, "45'" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int id = 1; id <= 13; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Tipo_Contenedor",
                    keyColumn: "Tipo_Contenedor_ID",
                    keyValue: id);
            }

            for (int id = 1; id <= 3; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Tamanio_Contenedor",
                    keyColumn: "Tamanio_Contenedor_ID",
                    keyValue: id);
            }

            migrationBuilder.AlterColumn<int>(
                name: "Numero_contenedor",
                table: "Cotizacion_Contenedor",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
