
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Estatus_Poliza")]
    public class EstatusPoliza
    {
        [Key]
        [Column("Estatus_Poliza_ID")]
        public int EstatusPolizaId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
