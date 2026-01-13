using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Tipo_Seguro")]
    public class TipoSeguro
    {
        [Key]
        [Column("Tipo_Seguro_ID")]
        public int TipoSeguroId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
