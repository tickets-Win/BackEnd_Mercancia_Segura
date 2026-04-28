using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Tipo_Contenedor")]
    public class TipoContenedor
    {
        [Key]
        [Column("Tipo_Contenedor_ID")]
        public int TipoContenedorId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

    }
}
