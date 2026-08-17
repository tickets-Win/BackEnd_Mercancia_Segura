using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    /// <summary>
    /// Categoría bajo la que se agrupan las plantillas de correo.
    ///
    /// OJO: igual que Certificado, esta clase se escribió contra el esquema real
    /// de la tabla en el servidor. Las migraciones que la crearon no están en
    /// este repositorio, así que aquí no hay una migración que la respalde.
    /// </summary>
    [Table("Categoria_Plantilla")]
    public class CategoriaPlantilla
    {
        [Key]
        [Column("Categoria_Plantilla_ID")]
        public int CategoriaPlantillaId { get; set; }

        [Column("Nombre")]
        [MaxLength(60)]
        public string? Nombre { get; set; }

        /// <summary>Las de sistema no se pueden borrar desde la pantalla.</summary>
        [Column("Es_Sistema")]
        public bool EsSistema { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
