using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercanciaSegura.DOM.Migrations
{
    /// <summary>
    /// Siembra el catálogo Tipo_Siniestro, que estaba vacío y deja inservible el
    /// combo del formulario de siniestros.
    ///
    /// OJO: al generar esta migración, EF también propuso reformar la tabla
    /// Certificado (renombrar Fecha_Certificado a Fecha_Registro y agregar
    /// Asegurado, Clave_Certificado, Fecha_Inicio, Fecha_Fin, Suma_Asegurada y
    /// Tipo_Estatus_ID). Esos cambios se quitaron a propósito: el servidor YA
    /// tiene esas columnas —las aplicaron migraciones de junio y julio que no
    /// están en este repositorio— y volver a aplicarlas fallaría. El snapshot sí
    /// se actualizó, que es lo que evita que la próxima migración vuelva a
    /// proponerlo.
    /// </summary>
    public partial class SeedTipoSiniestro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tipo_Siniestro",
                columns: new[] { "Tipo_Siniestro_ID", "Tipo" },
                values: new object[,]
                {
                    { 1, "Robo total" },
                    { 2, "Robo parcial" },
                    { 3, "Faltante" },
                    { 4, "Pérdida total" },
                    { 5, "Daño por manejo" },
                    { 6, "Daño por estiba" },
                    { 7, "Mojadura" },
                    { 8, "Contaminación" },
                    { 9, "Volcadura" },
                    { 10, "Colisión" },
                    { 11, "Incendio" },
                    { 12, "Caída de la carga" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int id = 1; id <= 12; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Tipo_Siniestro",
                    keyColumn: "Tipo_Siniestro_ID",
                    keyValue: id);
            }
        }
    }
}
