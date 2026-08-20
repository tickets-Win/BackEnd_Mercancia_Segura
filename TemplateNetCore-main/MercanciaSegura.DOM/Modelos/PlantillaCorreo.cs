using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    /// <summary>
    /// Plantilla de correo. El cuerpo se guarda en HTML, que es lo que produce el
    /// editor de la pantalla de envío.
    /// </summary>
    [Table("Plantilla_Correo")]
    public class PlantillaCorreo
    {
        [Key]
        [Column("Plantilla_Correo_ID")]
        public int PlantillaCorreoId { get; set; }

        [Column("Categoria_Plantilla_ID")]
        public int CategoriaPlantillaId { get; set; }

        [ForeignKey(nameof(CategoriaPlantillaId))]
        public CategoriaPlantilla? CategoriaPlantilla { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("Asunto")]
        [MaxLength(200)]
        public string? Asunto { get; set; }

        [Column("Cuerpo_HTML")]
        public string? CuerpoHtml { get; set; }

        /// <summary>Baja lógica: las inactivas no se ofrecen al enviar.</summary>
        [Column("Activa")]
        public bool Activa { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; }

        [Column("Fecha_Actualizacion")]
        public DateTime FechaActualizacion { get; set; }
    }
}
