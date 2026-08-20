using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <summary>
    /// Siembra el catálogo Categoria_Plantilla, que estaba vacío. Son las mismas
    /// siete categorías que la pantalla de Plantillas tenía fijas en código.
    ///
    /// OJO: al generarla, EF propuso además CreateTable para Categoria_Plantilla
    /// y Plantilla_Correo, con su índice y su llave foránea. Esas operaciones se
    /// quitaron a propósito: las dos tablas YA EXISTEN en el servidor —las creó
    /// una migración que no está en este repositorio— y volver a crearlas
    /// fallaría. El snapshot sí quedó actualizado, que es lo que evita que la
    /// próxima migración vuelva a proponerlo.
    /// </summary>
    public partial class SeedCategoriaPlantilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var ahora = new DateTime(2026, 8, 17);

            migrationBuilder.InsertData(
                table: "Categoria_Plantilla",
                columns: new[] { "Categoria_Plantilla_ID", "Nombre", "Es_Sistema", "Fecha_Registro" },
                values: new object[,]
                {
                    { 1, "Directorio", true, ahora },
                    { 2, "Documentos", true, ahora },
                    { 3, "Pagos", true, ahora },
                    { 4, "Endosos", true, ahora },
                    { 5, "Siniestros", true, ahora },
                    { 6, "Reclamaciones", true, ahora },
                    { 7, "General", true, ahora }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int id = 1; id <= 7; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Categoria_Plantilla",
                    keyColumn: "Categoria_Plantilla_ID",
                    keyValue: id);
            }
        }
    }
}
