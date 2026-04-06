using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Tipo_Siniestro")]
    public class TipoSiniestro
    {
        [Key]
        [Column("Tipo_Siniestro_ID")]
        public int TipoSiniestroId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
