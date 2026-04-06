using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Tipo_Endoso")]
    public class TipoEndoso
    {
        [Key]
        [Column("Tipo_ID")]
        public int TipoId { get; set; }

        [Column("Tipo")]
        [MaxLength(150)]
        public string? Tipo { get; set; }
    }
}